﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Crocodile
{
    public enum Category
    {
        profession, nature, other, conception, people, character, adult
    };

    class WordList
    {
        private static string[] PROFESSION =
        {
            "стриптизер", "сантехник", "сурдопереводчик", "пожарный", "дальнобойщик", "психиатр", "лифтер",
            "прокурор", "акушерка", "скульптор", "режиссер", "кинолог", "космонавт", "дипломат", "крановщик",
            "химик", "стюардесса", "шахтер", "пчеловод", "дизайнер", "электрик", "дрессировщик", "промоутер",
            "археолог", "ветеринар", "дровосек", "программист", "таксист", "продавец", "повар", "физик",
            "топ-модель", "дизайнер", "обувщик", "коммивояжер", "спасатель", "велосипедист", "агент", "каскадер",
            "следопыт", "актер"
        };

        private static string[] NATURE =
        {
            "хамелеон", "ротвейлер", "краб", "жук-навозник", "выхухоль", "морская звезда", "удав", "скунс",
            "саранча", "страус", "ленивец", "чихуахуа", "блоха", "енот", "креветка", "божья коровка", "муравьед",
            "утконос", "колибри", "бобр", "пеликан", "павлин", "гусеница", "паук", "динозавр", "медуза",
            "улитка", "индюк", "дикобраз", "шиншилла", "скорпион", "лисица", "муравей", "корова", "медведь",
            "овца", "лягушка", "жаба", "гадюка", "крот", "червь", "капуста", "лопух", "дуб", "крапива",
            "ель", "сосна", "осьминог", "муха", "шершень"
        };

        private static string[] OTHER =
        {
            "фарш", "антивирус", "спрей", "тюль", "зарплата", "шкаф-купе", "гайморит", "тандем", "фотошоп",
            "википедия", "Facebook", "кровать-раскладушка", "поляна", "изба", "бигуди", "гей-парад", "путешествие",
            "парковка", "тошнота", "мусор", "дичь", "метро", "унитаз", "тюрьма", "диктофон", "заноза", "бал",
            "пицца", "суши", "понос", "секундомер", "обрыв", "скала", "невесомость", "санузел", "свинец", "образ",
            "ранчо", "шаверма", "шашлык", "ресторан", "бар", "завещание", "погреб", "супермаркет", "арбуз",
            "мороженое", "ночь", "планета", "олимпиада", "дача", "туалет", "записка", "виолончель", "собеседование",
            "жалость", "шпага", "поход", "кий", "плов", "борщ", "молния", "барабан"
        };

        private static string[] CONCEPTION =
        {
            "менталитет", "цивилизация", "перспектива", "резонанс", "империя", "постоянство", "ресурс", "коммунизм",
            "нацизм", "атеизм", "многобожие", "наука", "история", "биология", "математика", "физика", "химия",
            "география", "психология", "рукоделие", "азарт", "материнство", "детство", "юность", "старость",
            "воскрешение", "оживленность", "целомудрие", "фетишизм", "паранойя", "логика", "карате", "галактика",
            "героизм", "меткость", "вакуум", "экскурсия", "исследование", "паника", "начало", "патриотизм",
            "жизнь", "выбор", "бунт", "загар", "стая"
        };

        private static string[] PEOPLE =
        {
            "Владимир Жириновский", "Никита Михалков", "Стас Михайлов", "Наполеон Бонапарт", "Леди Гага",
            "Элтон Джон", "Юрий Куклачев", "Майкл Джексон", "Юрий Гагарин", "Исаак Ньютон", "Тимати",
            "Джеки Чан", "Андрей Малахов", "Альберт Эйнштейн", "Иван Грозный", "Александр Пушкин",
            "Верка Сердючка", "Владимир Ленин", "Карл Маркс", "Стив Джобс", "Билл Гейтс", "Ван Гог",
            "Владимир Путин", "Дмитрий Медведев", "Иосиф Сталин", "Иосиф Кобзон", "Муслим Магомаев",
            "Федор Шаляпин", "Александр Розенбаум", "Леонардо ДиКаприо", "Александр Пушной"
        };

        private static string[] CHARACTER =
        {
            "чукча", "Чак Норрис", "кентавр", "гейша", "Терминатор", "Cтарик Хоттабыч", "гопник", "трансвестит",
            "Котопес", "Бэтмен", "бомж", "Баба-Яга", "Шрэк", "инопланетянин", "Супермен", "Гомер Симпсон", "мачо",
            "хоббит", "доктор Хаус", "светская львица", "Джек-Воробей", "русалка", "девушка по вызову", "Винни-Пух",
            "Фредди Крюгер", "гном", "Колобок", "домовой", "смурфик", "свинка Пеппа", "богач", "миллионер", "бабник",
            "золотая молодежь", "зомби", "поклонник", "интеллигент", "султан", "царь", "камикадзе", "канатоходец",
            "турист"
        };

        private static string[] ADULT =
        {
            "резиновая женщина", "презерватив", "гинеколог" // Add whatever you want! :)
        };

        public static void Shuffle(List<string> list)
        {
            var rnd = new Random();
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static Stack<string> GenerateWords(List<Category> categories)
        {
            var result = new Stack<string>();
            var allWords = new List<string>();

            if (categories.Contains(Category.profession))
                allWords.AddRange(PROFESSION);

            if (categories.Contains(Category.people))
                allWords.AddRange(PEOPLE);

            if (categories.Contains(Category.other))
                allWords.AddRange(OTHER);

            if (categories.Contains(Category.nature))
                allWords.AddRange(NATURE);

            if (categories.Contains(Category.conception))
                allWords.AddRange(CONCEPTION);

            if (categories.Contains(Category.character))
                allWords.AddRange(CHARACTER);

            if (categories.Contains(Category.adult))
                allWords.AddRange(ADULT);

            Shuffle(allWords);
            Shuffle(allWords);

            foreach (string word in allWords)
                result.Push(word);

            return result;
        }
    }
}