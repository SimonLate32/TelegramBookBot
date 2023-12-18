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
        static ITelegramBotClient bot = new TelegramBotClient("6387326882:AAGTnlU-0hRfmhYRFYzYcCSijrDnu57T0l4");

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

        public static async Task SendPsychologyBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Памяти Эрвина Гоффмана, удивительного друга и соратника
        Моей жене Мэри Энн Мейсон, верной наперснице и терпеливому критику
        Если что-то выглядит именно так, как и должно выглядеть по нашему представлению, то за этим, скорее всего, кроется обман; там же, где обман кажется совершенно явным, скорее всего, никакого обмана нет.
        Эрвин Гоффман. Стратегическое взаимодействие
        Нам более пристала не столько мораль, сколько необходимость выжить. На любом уровне, от самого отчаянного стремления спрятаться до поэтического восторга, лингвистическая способность скрывать, обманывать, напускать туману, выдумывать незаменима для сохранения равновесия человеческого сознания и развития человека в обществе…
        Георг Штайнер. После Вавилонского столпотворения
        Если бы ложь, подобно истине, была одноликою, наше положение было бы значительно легче. Мы считали бы в таком случае достоверным противоположное тому, что говорит лжец. Но противоположность истине обладает сотней тысяч обличий и не имеет пределов.
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }

        public static async Task SendBusinessBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Купите подписку, чтобы просматривать книги!
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }

        public static async Task SendHealthFitnessBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Купите подписку, чтобы просматривать книги!
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }

        public static async Task SendPersonalFinanceBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Купите подписку, чтобы просматривать книги!
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }
        public static async Task SendMotivationDisciplineBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Купите подписку, чтобы просматривать книги!
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }
        public static async Task SendSocialRelationshipsBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Купите подписку, чтобы просматривать книги!
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }
        public static async Task SendArtCreativityBookDetails(ChatId chatId)
        {
            string bookDetails = @"
        Купите подписку, чтобы просматривать книги!
    ";

            await bot.SendTextMessageAsync(chatId, bookDetails);
        }
        public static async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
        {
            var message = callbackQuery.Message;

            switch (callbackQuery.Data)
            {
                case "1_view_books":
                    // Handle the action for viewing books related to "Психология лжи"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Психология лжи");
                    break;
                case "1_view_book":
                    // Handle the action for viewing the detailed information about the psychology book
                    await SendPsychologyBookDetails(message.Chat.Id);
                    break;
                case "2_view_books":
                    // Handle the action for viewing books related to "Бизнес и карьера"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Бизнес и карьера");
                    break;
                case "2_view_book":
                    // Handle the action for viewing the detailed information about the business book
                    await SendBusinessBookDetails(message.Chat.Id);
                    break;
                case "3_view_books":
                    // Handle the action for viewing books related to "Здоровье и фитнес"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Здоровье и фитнес");
                    break;
                case "3_view_book":
                    // Handle the action for viewing the detailed information about the health and fitness book
                    await SendHealthFitnessBookDetails(message.Chat.Id);
                    break;
                case "4_view_books":
                    // Handle the action for viewing books related to "Личные финансы"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Личные финансы");
                    break;
                case "4_view_book":
                    // Handle the action for viewing the detailed information about the personal finance book
                    await SendPersonalFinanceBookDetails(message.Chat.Id);
                    break;
                case "5_view_books":
                    // Handle the action for viewing books related to "Мотивация и дисциплина"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Мотивация и дисциплина");
                    break;
                case "5_view_book":
                    // Handle the action for viewing the detailed information about the motivation and discipline book
                    await SendMotivationDisciplineBookDetails(message.Chat.Id);
                    break;
                case "6_view_books":
                    // Handle the action for viewing books related to "Социальные отношения"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Социальные отношения");
                    break;
                case "6_view_book":
                    // Handle the action for viewing the detailed information about the social relationships book
                    await SendSocialRelationshipsBookDetails(message.Chat.Id);
                    break;
                case "7_view_books":
                    // Handle the action for viewing books related to "Искусство и творчество"
                    await bot.SendTextMessageAsync(message.Chat.Id, "Here are the books related to Искусство и творчество");
                    break;
                case "7_view_book":
                    // Handle the action for viewing the detailed information about the art and creativity book
                    await SendArtCreativityBookDetails(message.Chat.Id);
                    break;
                // Add cases for other genres
                default:
                    // Default behavior, add a button to view a specific book in the current genre
                    var keyboard = new InlineKeyboardMarkup(new[]
                    {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Просмотреть книгу", $"{callbackQuery.Data}_view_book"),
                },
            });

                    // Send a message with the selected genre name and the option to view a specific book
                    await bot.SendTextMessageAsync(message.Chat.Id, $"Вы выбрали: {GetGenreName(int.Parse(callbackQuery.Data))}", replyMarkup: keyboard);
                    break;
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
                    return "1. Психология лжи";
                case 2:
                    return "Купите подписку для дальнейшего просмотра книг!";
                case 3:
                    return "Купите подписку для дальнейшего просмотра книг!";
                case 4:
                    return "Купите подписку для дальнейшего просмотра книг!";
                case 5:
                    return "Купите подписку для дальнейшего просмотра книг!";
                case 6:
                    return "Купите подписку для дальнейшего просмотра книг!";
                case 7:
                    return "Купите подписку для дальнейшего просмотра книг!";
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
