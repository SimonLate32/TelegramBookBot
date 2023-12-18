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
        private static readonly ITelegramBotClient bot = new TelegramBotClient("6387326882:AAGTnlU-0hRfmhYRFYzYcCSijrDnu57T0l4");

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
                await HandleCallbackQueryAsync(update.CallbackQuery);
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
                    InlineKeyboardButton.WithCallbackData("2. Бизнес и карьера", "2"),
                    InlineKeyboardButton.WithCallbackData("3. Здоровье и фитнес", "3"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("4. Личные финансы", "4"),
                    InlineKeyboardButton.WithCallbackData("5. Мотивация и дисциплина", "5"),
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
            var data = callbackQuery.Data;

            if (data.StartsWith("view_books"))
            {
                var genre = int.Parse(data.Split('_')[0]);
                await bot.SendTextMessageAsync(message.Chat.Id, $"Here are the books related to {GetGenreName(genre)}");
            }
            else if (data.EndsWith("_view_book"))
            {
                var genre = int.Parse(data.Split('_')[0]);
                await SendBookDetails(message.Chat.Id, genre);
            }
            else
            {
                await HandleDefaultCallbackQuery(message, data);
            }
        }

        private static async Task SendBookDetails(ChatId chatId, int genre)
        {
            string bookDetails;

            switch (genre)
            {
                case 1:
                    bookDetails = @"
                        Памяти Эрвина Гоффмана, удивительного друга и соратника
                        Моей жене Мэри Энн Мейсон, верной наперснице и терпеливому критику
                        Если что-то выглядит именно так, как и должно выглядеть по нашему представлению, то за этим, скорее всего, кроется обман; там же, где обман кажется совершенно явным, скорее всего, никакого обмана нет.
                        Эрвин Гоффман. Стратегическое взаимодействие
                        Нам более пристала не столько мораль, сколько необходимость выжить. На любом уровне, от самого отчаянного стремления спрятаться до поэтического восторга, лингвистическая способность скрывать, обманывать, напускать туману, выдумывать незаменима для сохранения равновесия человеческого сознания и развития человека в обществе…
                        Георг Штайнер. После Вавилонского столпотворения
                        Если бы ложь, подобно истине, была одноликою, наше положение было бы значительно легче. Мы считали бы в таком случае достоверным противоположное тому, что говорит лжец. Но противоположность истине обладает сотней тысяч обличий и не имеет пределов.
                    ";
                    break;
                case 2:
                    bookDetails = "Купите подписку, чтобы просматривать книги!";
                    // Handle the action for viewing the detailed information about the business book
                    break;
                case 3:
                    bookDetails = "Купите подписку, чтобы просматривать книги!";
                    // Handle the action for viewing the detailed information about the health and fitness book
                    break;
                case 4:
                    bookDetails = "Купите подписку, чтобы просматривать книги!";
                    // Handle the action for viewing the detailed information about the personal finance book
                    break;
                case 5:
                    bookDetails = "Купите подписку, чтобы просматривать книги!";
                    // Handle the action for viewing the detailed information about the motivation and discipline book
                    break;
                case 6:
                    bookDetails = "Купите подписку, чтобы просматривать книги!";
                    // Handle the action for viewing the detailed information about the social relationships book
                    break;
                case 7:
                    bookDetails = "Купите подписку, чтобы просматривать книги!";
                    // Handle the action for viewing the detailed information about the art and creativity book
                    break;
                default:
                    bookDetails = "Нет доступной информации для этой книги";
                    break;
            }

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }

        private static async Task HandleDefaultCallbackQuery(Message message, string data)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Просмотреть книгу", $"{data}_view_book"),
                },
            });

            await bot.SendTextMessageAsync(message.Chat.Id, $"Вы выбрали: {GetGenreName(int.Parse(data))}", replyMarkup: keyboard);
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

        static void Main(string[] args)
        {
            Console.WriteLine($"Запущен бот {bot.GetMeAsync().Result.FirstName}");

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
