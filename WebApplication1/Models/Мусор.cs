using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /*db.WordSets.Load();
            db.WordSets.Add(Rand);
            db.SaveChanges();*/
    /*{//Удаление данных из бд
        db.WordSets.Load();//загрузил бд
        var word = db.WordSets.Local.ToBindingList(); //передал все данные в word
        var count = word.Count; //узнал количество данных
        if (count != 0)//Если там есть записи, то удаляем все
        {
            count--; //уменьшил на 1, т.к. счет начинается с 0
            //if (word[count].answer == "Yes")
                for (int a = count; a >= 0; a--)
                {
                    db.WordSets.Local.RemoveAt(a);
                    //db.WordSets.AsNoTracking();
                    db.SaveChanges();
                }
        }
    }*/

    /*private string Answer = "No";
        //private string[] Answer = new string[30];
        public string answer
        {
            get
            {
                if (string.Compare(Word, Rand, true) == 0) return Answer = "Yes";
                if (Num % 1 == 0) Answer = "Ну не, не то";
                if (Num % 2 == 0) Answer = "Не-а";
                if (Num % 3 == 0)
                    if (string.Compare(Word, Rand, false) < 0) Answer = "Слово длиннее";
                    else Answer = "Слово короче";
                if (Num % 4 == 0) Answer = "А вот и нет";
                if (Num % 5 == 0)
                {
                    int temp = Num / 5;
                    Answer = Convert.ToString(temp) + " Буква " + Convert.ToString(Rand[temp-1]);
                }
                return Answer;
            }
        }*/
}