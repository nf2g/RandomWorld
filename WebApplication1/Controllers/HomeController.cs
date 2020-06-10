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

            ViewBag.Random = new Scripts().RandWord();
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
            word[0].answer = new Scripts().Answer(WordSet, word.Count);//Генерирует ответ на вводимое слово
            //view
            ViewBag.date = word.OrderByDescending(p => p.Num);//передача данных объектов в представление отсортированное в обратном порядке
            ViewBag.Попытка = word.Count;//Для вывода попытки
            ViewBag.Random = word[0].Rand;//Для вывода загаданного слова

            db.SaveChanges();//сохраниние базы

            if (word[0].answer == "Yes")//Если угадал слово
            {
                ViewBag.Random = new Scripts().RandWord();//изменил загаданное слово
                ViewBag.Yes = true; //для отображения соответствующего сообщения
            }
            else ViewBag.Yes = false;

            return View();
        }

    }
}