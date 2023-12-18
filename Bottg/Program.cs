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
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message != null && message.Text != null)
                {
                    switch (message.Text.ToLower())
                    {
                        case "/start":
                            await SendStartMessageAsync(message.Chat, message.From.Username);
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
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static async Task SendStartMessageAsync(ChatId chatId, string username)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("1. Психология и самопомощь", "1"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("2. Бизнес и карьера", "2"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("3. Здоровье и фитнес", "3"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("4. Личные финансы", "4"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("5. Мотивация и дисциплина", "5"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("6. Социальные отношения", "6"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("7. Искусство и творчество", "7"),
                },
            });

            await bot.SendTextMessageAsync(
                chatId,
                $"📚 Добро пожаловать в мир знаний, {username}! 📚\r\n\r\nЯ – твой верный компаньон в литературном путешествии. Здесь ты можешь найти рекомендации, обсудить любимые произведения, и даже открыть для себя что-то новенькое. Готов начать?  Выбери один из жанров саморазвития:",
                replyMarkup: keyboard
            );
        }

        public static async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            var message = callbackQuery.Message;

            if (int.TryParse(callbackQuery.Data, out int selectedGenre))
            {
                string genreName = GetGenreName(selectedGenre);
                string bookList = GetBookList(selectedGenre);

                // Send a message with the selected genre name and book list
                await bot.SendTextMessageAsync(message.Chat.Id, $"Вы выбрали: {genreName}\n\n{bookList}");
            }
        }

        private static string GetGenreName(int selectedGenre)
        {
            switch (selectedGenre)
            {
                case 1:
                    return "Психология и самопомощь";
                case 2:
                    return "Бизнес и карьера";
                case 3:
                    return "Здоровье и фитнес";
                case 4:
                    return "Личные финансы";
                case 5:
                    return "Мотивация и дисциплина";
                case 6:
                    return "Социальные отношения";
                case 7:
                    return "Искусство и творчество";
                default:
                    return "Неизвестный жанр";
            }
        }

        private static string GetBookList(int selectedGenre)
        {
            switch (selectedGenre)
            {
                case 1:
                    return "1. Книга 1\n2. Книга 2\n3. Книга 3";
                case 2:
                    return "1. Книга A\n2. Книга B\n3. Книга C";
                case 3:
                    return "1. Книга A\n2. Книга B\n3. Книга C";
                case 4:
                    return "1. Книга A\n2. Книга B\n3. Книга C";
                case 5:
                    return "1. Книга A\n2. Книга B\n3. Книга C";
                case 6:
                    return "1. Книга A\n2. Книга B\n3. Книга C";
                case 7:
                    return "1. Книга A\n2. Книга B\n3. Книга C";
                default:
                    return "Нет доступного списка книг для этого жанра";
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
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
