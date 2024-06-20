//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using MySql.Data.MySqlClient;
//using System.Data.Common;
//using UnityEditor.MemoryProfiler;
//using System;
//using System.Text;
//using System.Threading.Tasks;
//public class StatisticController : MonoBehaviour
//{
//    private List<Cat> cats;
//    public string connectionString= "Server=localhost;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
//    private string v2 = "server=localhost;Port=3306;database=MeowLand;user=root;password=123123;Charset=utf8";
//    private MySqlConnection MS_Connection;
//    private MySqlCommand MS_Command;
//    private string query;

//    private void Awake()
//    {
//      cats = FindObjectsOfType<Cat>().ToList();
//        foreach (Cat cat in cats)
//        {
//            cat.Clicked.AddListener(OnCatClick);
//        }
//    }
//    private void OnCatClick(Cat cat)
//    {
//        Debug.Log("Clicked");
//        Connection();
//        Debug.Log("Connected");

//        string queryGet = "select  * from Cats";
//        MS_Command = new MySqlCommand(query,MS_Connection);
//        //MS_Command.ExecuteNonQuery();
//        MySqlDataReader MS_Reader = MS_Command.ExecuteReader();
//        StringBuilder builder= new StringBuilder();
//        while (MS_Reader.Read())
//        {
//            for (int i = 0; i < MS_Reader.FieldCount; i++)
//            {
//                builder.AppendLine(MS_Reader[i].ToString());
//            }
//            Debug.Log(builder.ToString());
//            builder.Clear();
//        }
//        MS_Connection.Close();
//    }
//    public void Connection()
//    {
//        Debug.Log("Connecting");
//        MS_Connection = new MySqlConnection(v2);
//        try { MS_Connection.Open(); } catch (Exception e){ Debug.Log(e); }
       
//    }
//}
