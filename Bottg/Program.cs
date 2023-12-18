using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;

namespace TelegramBotExperiments
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("6387326882:AAEwmVxsbA1FIEB8aL04EMxAzVu-lcjGkqw");

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message != null && message.Text != null)
                {
                    switch (message.Text.ToLower())
                    {
                        case "/start":
                            await SendStartMessageAsync(message.Chat);
                            break;

                        default:
                            await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
                            break;
                    }
                }
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var callbackQuery = update.CallbackQuery;
                await HandleCallbackQueryAsync(callbackQuery);
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static async Task SendStartMessageAsync(ChatId chatId)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Начнем!", "/book"),
                }
            });

            await bot.SendTextMessageAsync(
                chatId, 
                "Я – твой верный компаньон в литературном саморазвитии. Здесь ты можешь найти огромное количество книг, скачать любимые произведения, и даже открыть для себя что-то новенькое. Готовы начать?",
                replyMarkup: keyboard
            );
        }

        public static async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            var message = callbackQuery.Message;
            if (callbackQuery.Data == "/book")
            {
                var genresList = @"Выбери один из пунктов:

1. Психология и Самопомощь: Книги об осознанности, развитии личности, управлении стрессом, преодолении трудностей и создании позитивного мышления.

2. Бизнес и Карьера: Литература о предпринимательстве, лидерстве, управлении временем, развитии навыков коммуникации, эффективности работы и т.д.

3. Здоровье и Фитнес: Книги о здоровом образе жизни, питании, спорте, медитации и общем физическом благополучии.

4. Личные Финансы: Ресурсы о финансовом планировании, инвестировании, управлении долгами и создании стабильного финансового будущего.

5. Мотивация и Самодисциплина: Книги, которые помогут вам находить вдохновение, устанавливать и достигать целей, развивать самодисциплину.

6. Образование и Новые Навыки: Литература о самообразовании, изучении новых навыков, повышении квалификации и постоянном обучении.

7. Социальные отношения: Книги о межличностных отношениях, общении, любви и взаимопонимании.

8. Религия и Духовное Развитие: Ресурсы, посвященные духовному росту, медитации, практикам осознанности и т.п.

9. Искусство и Творчество: Книги, которые помогут вам развивать творческий потенциал, вдохновляться и раскрывать свой творческий потенциал.";

                await bot.SendTextMessageAsync(message.Chat.Id, genresList);
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
