using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace Validation_Utility {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) {
      textBox1_doText(textBox1, e);
    }
    ///private void testf(object sender, EventArgs e);
    public void LoadJsonIBAN(string param) {
      param = "http://openapi.ro/api/validate/iban/" + param + ".json";
      Uri p = new Uri(param);
      using (var client = new WebClient())
        client.DownloadFile(p, "file.json");
      using (StreamReader r = new StreamReader("file.json")) {
        string json = r.ReadToEnd();
        dynamic array = JsonConvert.DeserializeObject(json);
        foreach (var item in array) {
          if (item.First == true)
            System.Windows.Forms.MessageBox.Show("IBAN CORECT!");
          else
            System.Windows.Forms.MessageBox.Show("IBAN GRESIT!");
        }
      }
    }

    public void LoadJsonPC(string param) {
      param = "http://openapi.ro/api/addresses.json?zip=" + param;
      Uri p = new Uri(param);
      using (var client = new WebClient())
        client.DownloadFile(p, "file3.json");
      using (StreamReader r = new StreamReader("file3.json")) {
        string json = r.ReadToEnd();
        dynamic array = JsonConvert.DeserializeObject(json);
        string ans = "";
        foreach (var x in array) {
          foreach (var item in x) {
            string aux1 = (string)item.Name, aux2 = "";
            bool ok = false;
            foreach (char ch in aux1) {
              char carac = ch;
              if (carac == '_') carac = ' ';
              if (Char.IsLetter(carac) || carac == ' ') {
                if (ok == false) {
                  aux2 = aux2 + Char.ToUpper(carac);
                  ok = true;
                } else
                  aux2 = aux2 + carac;
              }
            }

            aux2 = aux2 + ": " + (string)item.First;
            ans = ans + aux2 + "\n";
          }

        }
        System.Windows.Forms.MessageBox.Show(ans);
      }
    }
      
    public void GiveMeInfo(string param) {
      param = "http://openapi.ro/api/companies/" + param + ".json";
      Uri p = new Uri(param);
      using(var client = new WebClient())
        client.DownloadFile(p, "file2.json");
      using(StreamReader r = new StreamReader("file2.json")) {
        string json = r.ReadToEnd();
        dynamic array = JsonConvert.DeserializeObject(json);
        string ans = "";
        foreach(var item in array) {
          string aux1 = (string)item.Name, aux2 = "";
          bool ok = false;
          foreach (char ch in aux1) {
            char carac = ch;
            if (carac == '_') carac = ' ';
            if (Char.IsLetter(carac) || carac == ' ') {
              if (ok == false) {
                aux2 = aux2 + Char.ToUpper(carac);
                ok = true;
              } else
                aux2 = aux2 + carac;
            }
          }
                      
          aux2 = aux2 + ": " + (string)item.First;
          ans = ans + aux2 + "\n";
       }
       System.Windows.Forms.MessageBox.Show(ans);
     }
    }

    private void textBox1_doText(object sender, EventArgs e) {
      TextBox t = (TextBox)sender;
      string text = t.Text;
      LoadJsonIBAN(text);
    }
    private void textBox1_TextChanged(object sender, EventArgs e) { }

    private void button2_Click(object sender, EventArgs e) {
      string text = textBox2.Text;
      GiveMeInfo(text);
    }

    private void label3_Click(object sender, EventArgs e) { }

    private void button3_Click(object sender, EventArgs e) {
      string text = textBox3.Text;
      LoadJsonPC(text);
    }
  }
}
