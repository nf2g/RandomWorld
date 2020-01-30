using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();//создаю базу

        [HttpGet]
        public ActionResult Index()
        {
            db.WordSets.RemoveRange(db.WordSets);//очищаю базу
            db.SaveChanges();//сохраняю

            ViewBag.Random = RandWord();
            ViewBag.Попытка = 0;

            return View();
        }

        [HttpPost]
        public ActionResult Index(WordSet WordSet)
        {
            WordSet.Word = WordSet.Word.Trim(' ');//обрезает пробелы в начале и конце

            db.WordSets.Add(WordSet);//добавление в базу элементов с объекта
            db.WordSets.Load();//загрузка базы

            var word = db.WordSets.Local.ToBindingList();//передача данных в объект
            word[0].answer = Answer(WordSet, word.Count);//Генерирует ответ на вводимое слово
            //view
            ViewBag.date = word.OrderByDescending(p => p.Num);//передача данных объектов в представление отсортированное в обратном порядке
            ViewBag.Попытка = word.Count;//Для вывода попытки
            ViewBag.Random = word[0].Rand;//Для вывода загаданного слова

            db.SaveChanges();//сохраниние базы

            if (word[0].answer == "Yes")//Если угадал слово
            {
                ViewBag.Random = RandWord();//изменил загаданное слово
                ViewBag.Yes = true; //для отображения соответствующего сообщения
            }
            else ViewBag.Yes = false;

            return View();
        }

        //генерация числовой последовательности
        private string RandWord()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            string[] _word = {"Слон", "Жираф", "Крокодил", "Бегемот", "Олень", "Медведь", "Тюлень", "Попугай", "Акула", "Змея", 
                "Тигр", "Лев", "Обезъяна", "Гепард", "Панда", "Орел", "Ястреб", "Беркут", "Кошка", "Собака", "Лошадь", "Ленивец", 
                "Паук", "Гусеница", "Антилопа", "Кайот", "Верблюд", "Бык", "Корова", "Свинья", "Курица", "Петух", "Хомяк", "Крыса", 
                "Мышь", "Крот", "Енот", "Носорог"};
            //char[] word = new char[rand.Next(4, 21)];

            //А-Я 1040-1071 а-я 1072 1103
            //for (int i = 0; i < word.Length; ++i)
            //     word[i] = Convert.ToChar(rand.Next(1040, 1104));

            //return new string(word);
            return _word[rand.Next(0, _word.Length)];
        }

        private string Answer(WordSet WordSet, int Попытка)
        {
            if (string.Equals(WordSet.Word.ToLower(), WordSet.Rand.ToLower()))
                return "Yes";

            if (LevenshteinDistance(WordSet.Word, WordSet.Rand) <= 3 && Попытка % 5 != 0 && Попытка % 3 != 0)
                return "Ваше слово похоже на загаданное";

            if (Попытка % 5 == 0)
            {
                //Random random = new Random();
                //int rank = random.Next(0, WordSet.Word.Length + 1); //чтобы выдавал любую букву из слова
                return Convert.ToString(Попытка / 5) + " Буква " + Convert.ToString(WordSet.Rand[Попытка / 5 - 1]);
            }

            if (Попытка % 4 == 0)
                return "А вот и нет";

            if (Попытка % 3 == 0)
            {
                if (WordSet.Word.Length < WordSet.Rand.Length)
                    return "Слово длиннее";
                else if (WordSet.Word.Length == WordSet.Rand.Length)
                    return "Угадал количество букв";
                else return "Слово короче";
            }

            if (Попытка % 2 == 0)
                return "Не-а";

            if (Попытка % 1 == 0)
                return "Ну не, не то";

            return "Ошибка";
        }

        //Расстояние Левенштайна
        private static int LevenshteinDistance(string text1, int len1, string text2, int len2)
        {
            if (len1 == 0)
            {
                return len2;
            }

            if (len2 == 0)
            {
                return len1;
            }

            var substitutionCost = 0;
            if (text1[len1 - 1] != text2[len2 - 1])
            {
                substitutionCost = 1;
            }

            var deletion = LevenshteinDistance(text1, len1 - 1, text2, len2) + 1;
            var insertion = LevenshteinDistance(text1, len1, text2, len2 - 1) + 1;
            var substitution = LevenshteinDistance(text1, len1 - 1, text2, len2 - 1) + substitutionCost;

            return Minimum(deletion, insertion, substitution);
        }

        private static int LevenshteinDistance(string word1, string word2) =>
                LevenshteinDistance(word1, word1.Length, word2, word2.Length);

        private static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

    }
}