using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//情報を読み込むStringReaderを使用するために導入
//絵文字の基礎情報をCSVから読み込んで、変数に格納する
public class EmojiInformation//MonoBehaviourは継承しない
{
    static TextAsset csvFile;//CSVファイルを変数として扱うために宣言
    static List<string[]> emojiData = new List<string[]>();//CSVファイルの中身を入れる配列を定義。全てのデータが文字列形式で格納される
    //変数名[i]が絵文字IDがiの情報をそれぞれ示す
    public int[] emojiID = new int[129];//絵文字のID
    public int[] emojiAttribute1 = new int[129];//絵文字の属性1。感情(喜怒哀楽＋無を1～5で表す)
    public int[] emojiAttribute2 = new int[129];//絵文字の属性2。1つの分類ごとに3～5個で34個に分ける
    public string[] imageAddress = new string[129];//絵文字の画像イメージのアドレス
    //指定したアドレスに保管されているCSVファイルから情報を読み取り、emojiDataに情報を文字列として格納するメソッド。
    //emojiData[i][j]はCSVファイルのi行、j列目のデータを表す。但し先頭行（タイトル部分）は0行目と考えるものとする。
    static void CsvReader()
    {
        csvFile = Resources.Load("CSV/EmojiCSV") as TextAsset;//指定したファイルをTextAssetとして読み込み(ファイル名の.csvは不要なことに注意)　最初の行（タイトル部分）も読み込まれるのでそこは使用しない
        StringReader reader = new StringReader(csvFile.text);//
        while (reader.Peek() != -1)//最後まで読み込むと-1になる関数
        {
            string line = reader.ReadLine();//一行ずつ読み込み
            emojiData.Add(line.Split(','));//,区切りでリストに追加していく
        }
    }
    //emojiDataに一度CSVファイルのデータを読み込んだら他のプログラムから扱いやすいよう定義したemojiID等の変数にデータを格納する
    public void Init()
    {
        CsvReader();//emojiDataへ情報を一時格納
        //各変数へデータを格納
        for (int i = 1; i < emojiData.Count; i++)//絵文字IDが記述された最後まで読み込み。一行目はタイトルなのでi=0はデータとして扱わない
        {
            emojiID[i] = int.Parse(emojiData[i][0]);//string型からint型へ変換
            emojiAttribute1[i] = int.Parse(emojiData[i][1]);
            emojiAttribute2[i] = int.Parse(emojiData[i][2]);
            imageAddress[i] = emojiData[i][3];
        }
    }
}