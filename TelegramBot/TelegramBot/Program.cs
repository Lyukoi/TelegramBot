using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Keyboards;
using TelegramBotSQL;

public class Program
{
    private static ITelegramBotClient? _botClient;
    private static ReceiverOptions? _receiverOptions;

    static async Task Main()
    {
        _botClient = new TelegramBotClient("Your_Bot_Token");
        _receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery,
            },
            ThrowPendingUpdates = true,
        };

        SQL.CreateTable();
        using var cts = new CancellationTokenSource();

        _botClient.StartReceiving(HandleUpdate, ErrorHandler, _receiverOptions, cts.Token);

        var me = await _botClient.GetMeAsync();
        Console.WriteLine($"{me.Username} запущен!");

        await Task.Delay(-1);
    }

    private static async Task HandleUpdate(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                await HandleMessageUpdate(botClient, update.Message, cancellationToken);
                break;
            case UpdateType.CallbackQuery:
                await HandleCallbackQueryUpdate(botClient, update.CallbackQuery, cancellationToken);
                break;
            default:
                break;
        }
    }
    private static async Task HandleMessageUpdate(ITelegramBotClient botClient, Message? receivedMessage, CancellationToken cancellationToken)
    {
        var message = receivedMessage;
        var user = message?.From;
        var chat = message?.Chat;
        var delay = Task.Delay(300);

        ReplyKeyboardMarkup general_keyboard = Keyboards.GetGeneralKeyboard();
        ReplyKeyboardMarkup mg_keyboard = Keyboards.GetMuscleGroupKeyboard();
        InlineKeyboardMarkup howtobp_keyboard = Keyboards.GetHowToBPKeyboard();
        InlineKeyboardMarkup howtocf_keyboard = Keyboards.GetHowToCFKeyboard();
        InlineKeyboardMarkup howtobc_keyboard = Keyboards.GetHowToBCKeyboard();
        InlineKeyboardMarkup howtopc_keyboard = Keyboards.GetHowToPCKeyboard();
        InlineKeyboardMarkup howtotp_keyboard = Keyboards.GetHowToTPKeyboard();
        InlineKeyboardMarkup howtosc_keyboard = Keyboards.GetHowToSCKeyboard();
        ReplyKeyboardMarkup TrainingPrograms_keyboard = Keyboards.GetTrainingProgramsKeyboard();

        Console.WriteLine($"{user?.FirstName} ({user?.Id}) написал сообщение: {message?.Text}");
            switch (message?.Text)
            {
            case "/start":
                await botClient.SendTextMessageAsync(chat!.Id, "Привет, я чат бот 🤖 и я здесь что бы помочь тебе с тренировками", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "С чего хочешь начать?", replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                SQL.RegisterUser('@' + message?.From?.Username);
                break;
            case "/help":
                await botClient.SendTextMessageAsync(chat!.Id, "У меня есть такие команды:" + Environment.NewLine + "/start" + Environment.NewLine + "/help", cancellationToken: cancellationToken);
                break;
            case "/get_users":
                string userStr = string.Empty;
                foreach (var users in SQL.GetUsers())
                {
                    userStr += user + Environment.NewLine;
                }
                Message sentMessage = await botClient.SendTextMessageAsync(message.Chat.Id,
                    $"Пользователи:\n{userStr}", cancellationToken: cancellationToken);
                break;
            case "Библиотека Упражнений 📚":
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Загружаю библиотеку упражнений", cancellationToken: cancellationToken);
                await delay;
                await botClient.SendTextMessageAsync(chat.Id, "Выбери на какую группу мышц хочешь увидеть упражнения", replyMarkup: mg_keyboard, cancellationToken: cancellationToken);
                break;
            case "Грудные":
                ReplyKeyboardMarkup chest_keyboard = new(new[]
                {
                                    new KeyboardButton[] { "Жим штанги лёжа", "Сведение рук на тренажёре-бабочке"},
                                    new KeyboardButton[] {"В библиотеку упражнений"},
                                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(chat!.Id, "Выберите упражнение", replyMarkup: chest_keyboard, cancellationToken: cancellationToken);
                break;
            case "Жим штанги лёжа":
                await delay;
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://s3assets.skimble.com/assets/2289478/image_iphone.jpg"), caption: "Жим штанги лёжа\n Основная группа мышц: Грудные\n Второстепенная группа мышц: Трицепс, Плечи", replyMarkup: howtobp_keyboard, cancellationToken: cancellationToken);
                break;
            case "Сведение рук на тренажёре-бабочке":
                await delay;
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://i.pinimg.com/564x/ed/4d/1f/ed4d1f6ce1bdbb4cda128ac708272500.jpg"), caption: "Сведение рук на тренажёре-бабочке\n Основная группа мышц: Грудные", replyMarkup: howtocf_keyboard, cancellationToken: cancellationToken);
                break;
            case "Бицепс":
                ReplyKeyboardMarkup biceps_keyboard = new(new[]
                {
                    new KeyboardButton[] { "Подъёмы штанги", "Сгибания на Скамье Скотта"},
                    new KeyboardButton[] {"В библиотеку упражнений" },
                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(chat!.Id, "Выберите упражнение", replyMarkup: biceps_keyboard, cancellationToken: cancellationToken);
                break;
            case "Подъёмы штанги":
                await delay;
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/barbell-curl/barbell-curl-800.jpg"), caption: "Подъёмы штанги\n Основная группа мышц: Бицепс", replyMarkup: howtobc_keyboard, cancellationToken: cancellationToken);
                break;
            case "Сгибания на Скамье Скотта":
                await delay;
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://cdn.shopify.com/s/files/1/1497/9682/files/Close-Grip_Preacher_Curls.jpg?v=1669134373"), caption: "Сгибания на Скамье Скотта\n Основная группа мышц: Бицепс", replyMarkup: howtopc_keyboard, cancellationToken: cancellationToken);
                break;
            case "Трицепс":
                ReplyKeyboardMarkup triceps_keyboard = new(new[]
                {
                                    new KeyboardButton[] { "Разгибание рук на блоке", "Французский жим"},
                                    new KeyboardButton[] {"В библиотеку упражнений"},
                                })
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(chat!.Id, "Выберите упражнение", replyMarkup: triceps_keyboard, cancellationToken: cancellationToken);
                break;
            case "Разгибание рук на блоке":
                await delay;
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/tricep-rope-pushdown/tricep-rope-pushdown-800.jpg"), caption: "Разгибание рук на блоке\n Основная группа мышц: Трицепс", replyMarkup: howtotp_keyboard, cancellationToken: cancellationToken);
                break;
            case "Французский жим":
                await delay;
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://adventurefitness.club/wp-content/uploads/2022/11/lying-triceps-extension-vs-skullcrusher.jpeg"), caption: "Французский жим (Штанга)\n Основная группа мышц: Трицепс", replyMarkup: howtosc_keyboard, cancellationToken: cancellationToken);
                break;
            case "В библиотеку упражнений":
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Возвращаю в библиотеку упражнений", replyMarkup: mg_keyboard, cancellationToken: cancellationToken);
                break;
            case "Программы тренировок 💪":
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Отлично, теперь выбери тип тренировки:", replyMarkup: TrainingPrograms_keyboard, cancellationToken: cancellationToken);
                break;
            case "3 дня Фулл Боди":
                await delay;
                ReplyKeyboardMarkup fbs_keyboard = new(new[]
                {
                    new KeyboardButton[]{"Начать первый день трехдневной фулл боди тренировки."},
                    new KeyboardButton[]{"Начать второй день трехдневной фулл боди тренировки."},
                    new KeyboardButton[]{"Начать третий день трехдневной фулл боди тренировки."},
                    new KeyboardButton[]{ "В меню программ тренировок" }
                })
                {
                    OneTimeKeyboard = true,
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(chat!.Id, "Выбери день:", replyMarkup: fbs_keyboard);
                break;
            case "Начать первый день трехдневной фулл боди тренировки.":
                InlineKeyboardMarkup NextKeyboardFBS1 = Keyboards.GetNextKeyboardFBS1();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Запускаю первый день программы Фулл Боди", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://s3assets.skimble.com/assets/2289478/image_iphone.jpg"), caption: "После разминки постарайтесь сделать 8–10 повторений. Если вы не можете сделать 8 повторений, уменьшите вес. Если вы можете сделать больше 10 повторений, увеличьте вес. Применяйте это для всех упражнений в этой программе.", replyMarkup: howtobp_keyboard, cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardFBS1, cancellationToken: cancellationToken);
                break;
            case "Начать второй день трехдневной фулл боди тренировки.":
                InlineKeyboardMarkup NextKeyboard2FBS1 = Keyboards.GetNextKeyboard2FBS1();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Запускаю второй день программы Фулл Боди", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/incline-dumbbell-bench-press/incline-dumbbell-bench-press-800.jpg"), caption: "После разминки постарайтесь сделать 8–10 повторений. Если вы не можете сделать 8 повторений, уменьшите вес. Если вы можете сделать больше 10 повторений, увеличьте вес. Применяйте это для всех упражнений в этой программе.\n1. Установите наклон скамьи примерно на 45 градусов, возьмите пару гантелей и сядьте.\n2. Держа гантели в каждой руке и положив их на бедра, медленно лягте на скамью, одновременно ударяя обе гантели. выше вашего туловища. Ваши ступни, ягодицы, верхняя часть спины и голова должны соприкасаться со скамьей.\n3. Разведите обе гантели по бокам, твердо поставьте ступни, отведите плечи назад и выпрямите грудь. Поясница здесь должна быть слегка прогнута, а локти должны быть разведены в стороны, но не разведены в стороны.\n4.Из этого положения сделайте вдох и подтолкните обе гантели вверх к потолку и внутрь, пока они слегка не соприкоснутся.\n5. Выдохните и верните их в стороны.\n6.Продолжайте повторять.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard2FBS1, cancellationToken: cancellationToken);
                break;
            case "Начать третий день трехдневной фулл боди тренировки.":
                InlineKeyboardMarkup NextKeyboard3FBS1 = Keyboards.GetNextKeyboard3FBS1();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Запускаю третий день программы Фулл Боди", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://sporium.net/wp-content/uploads/2020/11/Cable-Crossover-nereyi-calistirir.jpg"), caption: "После разминки постарайтесь сделать 8–10 повторений. Если вы не можете сделать 8 повторений, уменьшите вес. Если вы можете сделать больше 10 повторений, увеличьте вес. Применяйте это для всех упражнений в этой программе.\n1. Отрегулируйте нагрузку на пару грузовых блоков внутри машины для пересечения тросов.\n2.Установите шкивы в самое верхнее положение и прикрепите ручки к обоим.\n3.Возьмитесь за ручки по одной и расположитесь посередине между ними. две опоры.\n4.Поднимите руки по бокам и сделайте полшага вперед, чтобы поднять гирю из стопок.\n5.Напрягите пресс, отведите плечи назад и вдохните\n6.Подведите руки внутрь и вниз. , встретив костяшки пальцев перед бедрами.\n7. Задержитесь на мгновение и на выдохе поднимите руки в стороны.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard3FBS1, cancellationToken: cancellationToken);
                break;
            case "3 дня Сплит":
                await delay;
                ReplyKeyboardMarkup split_keyboard = new(new[]
                {
                    new KeyboardButton[]{"Начать первый день трехдневной сплит-тренировки."},
                    new KeyboardButton[]{"Начать второй день трехдневной сплит-тренировки."},
                    new KeyboardButton[]{"Начать третий день трехдневной сплит-тренировки."},
                    new KeyboardButton[]{"В меню программ тренировок"}
                })
                {
                    OneTimeKeyboard = true,
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(chat!.Id, "Выбери день:", replyMarkup: split_keyboard, cancellationToken: cancellationToken);
                break;
            case "Начать первый день трехдневной сплит-тренировки.":
                InlineKeyboardMarkup NextKeyboardSplit1_1 = Keyboards.GetNextKeyboardSplitDay1_1();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Запускаю первый день программы Сплит", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://s3assets.skimble.com/assets/2289478/image_iphone.jpg"), caption: "После разминки постарайтесь сделать 8–10 повторений. Если вы не можете сделать 8 повторений, уменьшите вес. Если вы можете сделать больше 10 повторений, увеличьте вес. Применяйте это для всех упражнений в этой программе.\n1. Лягте на скамью.\n2.Вытяните руки и равномерно возьмитесь за перекладину, расставив руки чуть шире плеч.\n3.Отведите лопатки назад и упритесь ими в скамью.\n4.Согните нижнюю часть тела. откиньтесь назад и поставьте ступни на пол.\n5.Вдохните, снимите штангу и перенесите ее на грудь.\n6.Снова вдохните и опустите штангу к нижней части груди, слегка постукивая по ней.\n7.Удерживайте на мгновение и нажмите на штангу, пока локти не станут прямыми. Выдохните.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit1_1, cancellationToken: cancellationToken);
                break;
            case "Начать второй день трехдневной сплит-тренировки.":
                InlineKeyboardMarkup NextKeyboardSplit2_1 = Keyboards.GetNextKeyboardSplitDay2_1();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Запускаю второй день программы Сплит", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://images.squarespace-cdn.com/content/v1/55e406fbe4b0b03c5e7543ae/1500547685239-22WB8YGJZLO6805RK2TL/Bent+Bar+Seated+Cable+Row"), caption: "После разминки постарайтесь сделать 8–10 повторений. Если вы не можете сделать 8 повторений, уменьшите вес. Если вы можете сделать больше 10 повторений, увеличьте вес. Применяйте это для всех упражнений в этой программе.\n1.Выберите подходий вес и сядьте.\n2.Протянитесь вперед и равномерно возьмитесь за перекладину.\n3.Сядьте назад и поставьте ноги на подножки.\n4.Отведите плечи назад и напрягите пресс.\n5.Напрягитесь вдохните и потяните рукоятку к верхней части живота.\n6. Задержитесь на мгновение и полностью выпрямите руки на выдохе.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit2_1, cancellationToken: cancellationToken);
                break;
            case "Начать третий день трехдневной сплит-тренировки.":
                InlineKeyboardMarkup NextKeyboardSplit3_1 = Keyboards.GetNextKeyboardSplitDay3_1();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Запускаю третий день программы Сплит", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(chat!.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/03/kniebeugen-langhantel.png"), caption: "После разминки постарайтесь сделать 8–10 повторений. Если вы не можете сделать 8 повторений, уменьшите вес. Если вы можете сделать больше 10 повторений, увеличьте вес. Применяйте это для всех упражнений в этой программе.\n1.Расположите штангу на уровне ключиц.\n2.Возьмите штангу ровным хватом сверху (ладонями вниз).\n3.Подогните себя под штангу и поместите ее поверх трапеций (верхняя часть спины).\n4. Выровняйте ноги, напрягите пресс, сделайте вдох и снимите штангу, выпрямив ноги.\n5.Сделайте пару осторожных шагов назад и поставьте ноги на ширине плеч.\n6.Отведите плечи назад, вдохните, и приседайте, пока бедра не станут параллельны полу. Держите пятки прижатыми к полу.\n7.Задержитесь на мгновение и надавите на пятки, чтобы вернуться наверх.\n8.Выдохните около верхней точки.\n9.Завершив, двигайтесь вперед и осторожно переставьте штангу на стойку. прежде чем расслабить тело.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(chat!.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit3_1, cancellationToken: cancellationToken);
                break;
            case "Полезные Статьи 📰":
                InlineKeyboardMarkup ArticlesKeyboard = Keyboards.GetArticlesKeyboard();
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Загружаю статьи", replyMarkup: ArticlesKeyboard, cancellationToken: cancellationToken);
                break;
            // case с отзывом и опросником(poll message)
            case "В главное меню":
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Возвращаю в главное меню", replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            case "В меню программ тренировок":
                await delay;
                await botClient.SendTextMessageAsync(chat!.Id, "Выбери тип тренировки:", replyMarkup: TrainingPrograms_keyboard, cancellationToken: cancellationToken);
                break;
            default:
                Console.WriteLine($"Неизвестное сообщение: {message?.Text}");
                break;
        }
        return;
    }
    private static async Task HandleCallbackQueryUpdate(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        var callbackData = callbackQuery?.Data;
        ReplyKeyboardMarkup general_keyboard = Keyboards.GetGeneralKeyboard();
        Console.WriteLine($"{callbackQuery?.From.FirstName} || {callbackQuery?.Message?.Chat.Id} || {callbackQuery?.Message?.Date} || Нажал Кнопку Подробнее || {callbackQuery!.Message!.Chat.Id}");

        switch (callbackData)
        {
            case "howto_bp":
                await botClient.SendAnimationAsync(callbackQuery!.Message!.Chat.Id, animation: InputFile.FromUri("https://www.nickhallbodytransformations.com/wp-content/uploads/2019/01/00251301-Barbell-Bench-Press_Chest_360.gif"), caption: "1. Лягте на скамью.\n2.Вытяните руки и равномерно возьмитесь за перекладину, расставив руки чуть шире плеч.\n3.Отведите лопатки назад и упритесь ими в скамью.\n4.Согните нижнюю часть тела. откиньтесь назад и поставьте ступни на пол.\n5.Вдохните, снимите штангу и перенесите ее на грудь.\n6.Снова вдохните и опустите штангу к нижней части груди, слегка постукивая по ней.\n7.Удерживайте на мгновение и нажмите на штангу, пока локти не станут прямыми. Выдохните.", cancellationToken: cancellationToken);
                break;
            case "howto_cf":
                await botClient.SendAnimationAsync(callbackQuery!.Message!.Chat.Id, animation: InputFile.FromUri("https://i.pinimg.com/originals/a2/12/cd/a212cde8804175ee82be3abe83ca51e3.gif"), caption: "1. Выберите подходящую нагрузку и отрегулируйте высоту сиденья. Когда вы сидите, ручки тренажера должны находиться на уровне груди.\n2.Сядьте, отведите плечи назад и потянитесь в стороны, чтобы схватиться за ручки.\n3.Напрягите пресс и сделайте вдох.\n4.Напрягите грудь. и сведите руки, слегка постукивая костяшками пальцев перед грудью.\n5. Разведите руки в стороны, чувствуя, как растягиваются мышцы груди. Выдохните.", cancellationToken: cancellationToken);
                break;
            case "howto_bc":
                await botClient.SendAnimationAsync(callbackQuery!.Message!.Chat.Id, animation: InputFile.FromUri("https://fitnessprogramer.com/wp-content/uploads/2021/02/Barbell-Curl.gif"), caption: "1. Возьмите пару гантелей и встаньте прямо, ноги в удобной стойке, плечи втянуты.\n2.Направьте запястья вперед, а руки вытянуты.\n3.Сделайте вдох и одновременно согните обе гантели. Поднимайте гири до тех пор, пока запястья не окажутся немного выше локтей.\n4. Медленно вытяните руки и выдохните, опускаясь вниз.", cancellationToken: cancellationToken);
                break;
            case "howto_pc":
                await botClient.SendAnimationAsync(callbackQuery!.Message!.Chat.Id, animation: InputFile.FromUri("https://i.pinimg.com/originals/8f/03/87/8f03875e14e7d8687a575b648982352c.gif"), caption: "1. Загрузите перекладину и отрегулируйте высоту сиденья скамьи проповедника. Вы должны быть в состоянии положить плечи на подушку и удерживать туловище в вертикальном положении.\n2. Встаньте над тренажером, возьмите штангу ровным хватом снизу (ладони обращены к потолку), поднимите штангу и сядьте. \n3.Отведите плечи назад, напрягите пресс и вдохните.\n4.Поднимите штангу, напрягая бицепсы. Поднимайтесь вверх, пока предплечья не станут почти в вертикальном положении.\n5. Медленно вытягивайте руки на выдохе.", cancellationToken: cancellationToken);
                break;
            case "howto_tp":
                await botClient.SendAnimationAsync(callbackQuery!.Message!.Chat.Id, animation: InputFile.FromUri("https://newlife.com.cy/wp-content/uploads/2019/11/02001301-Cable-Pushdown-with-rope-attachment_Upper-Arms_360.gif"), caption: "1.Выберите подходящий вес, установите блок в самое верхнее положение и прикрепите веревку.\n2.Возьмите веревку обеими руками и разведите локти в стороны.\n3.Сделайте шаг назад и слегка наклоните туловище вперед. \n4.Сделайте вдох и выпрямите руки, сохраняя локти в нужном положении.\n5.Сожмите трицепсы и медленно согните руки, выдыхая при подъеме. Остановитесь, когда ваши запястья окажутся немного выше локтей.", cancellationToken: cancellationToken);
                break;
            case "howto_sc":
                await botClient.SendAnimationAsync(callbackQuery!.Message!.Chat.Id, animation: InputFile.FromUri("https://newlife.com.cy/wp-content/uploads/2019/11/00611301-Barbell-Lying-Triceps-Extension_Upper-Arms_360.gif"), caption: "1.Загрузите прямую штангу, поднимите ее с пола и поддержите перед грудью.\n2.Осторожно сядьте на плоскую спортивную скамью и лягте на спину, удерживая штангу близко к туловищу.\n3.Вытяните руки. и верните плечи назад. Поставьте ступни на пол.\n4.Сделайте вдох и опустите штангу ко лбу или за голову.\n5.Сделайте паузу на мгновение и вытяните руки, сохраняя локти на месте. Выдохните ближе к вершине.", cancellationToken: cancellationToken);
                break;
            ///////////////////
            case "FBSNext1":
                InlineKeyboardMarkup howtobp_keyboard = Keyboards.GetHowToBPKeyboard();
                InlineKeyboardMarkup NextKeyboardFBS2 = Keyboards.GetNextKeyboardFBS2();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 2 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/03/kniebeugen-langhantel.png"), caption: "Приседания со штангой\n\n1. Расположите штангу на уровне ключиц.\n\n2. Возьмите штангу ровным хватом сверху (ладонями вниз).\n\n3. Подогните себя под штангу и поместите ее на верхнюю часть трапеции (верхняя часть спины). \n\n4. Выровняйте ноги, напрягите пресс, сделайте вдох и снимите штангу, выпрямив ноги.\n\n5. Сделайте пару осторожных шагов назад и поставьте ноги на ширине плеч.\n\n6. Отведите плечи назад. , вдохните и приседайте, пока бедра не станут параллельны полу. Держите пятки на полу.\n\n7. Задержитесь на мгновение и надавите на пятки, чтобы вернуться наверх.\n\n8. Выдохните возле верхней точки.\n\n9. Завершив, двигайтесь вперед и осторожно переставьте штангу на стойку. прежде чем расслабить тело.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardFBS2, cancellationToken: cancellationToken);
                break;
            case "FBSNext2":
                InlineKeyboardMarkup NextKeyboardFBS3 = Keyboards.GetNextKeyboardFBS3();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 3 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://www.mybodycreator.com/content/files/2023/05/25/46_M.png"), caption: "1. Загрузите штангу и расположите ее на полу перед собой. \n2. Встаньте перед перекладиной, расположив ступни в удобной стойке и слегка направив пальцы ног наружу. \n3. Наклонитесь вперед и согните колени настолько, чтобы схватить перекладину двойным хватом сверху, не перемещая и не поднимая ее. \n4. Отведите плечи как можно дальше назад, чтобы выпрямить спину. Ваши плечи должны находиться немного впереди штанги и выше бедер. Исходное положение напоминает обычную становую тягу, с той лишь разницей, что расстояние между голенями и штангой небольшое.\n5. Вдохните и плавно подтяните штангу к груди.\n6. Прикоснитесь к туловищу штанги, удерживая верхнюю часть. положение на мгновение и отпустите.\n7. Медленно опустите штангу и положите ее на пол на выдохе.\n8. Сделайте еще один вдох и гребите штангу из полной остановки, делая паузу в верхней точке.\n9. Повторяйте до тех пор, пока все готово.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardFBS3, cancellationToken: cancellationToken);
                break;
            case "FBSNext3":
                InlineKeyboardMarkup NextKeyboardFBS4 = Keyboards.GetNextKeyboardFBS4();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 4 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/dumbbell-lateral-raise/dumbbell-lateral-raise-800.jpg"), caption: "1. Возьмите пару гантелей, которые достаточно легкие, чтобы вы могли сделать не менее двенадцати повторений за подход.\n2. Встаньте прямо, положив гантели по бокам и ладонями внутрь.\n3. Отведите плечи назад, направьте взгляд вперед. , и перевести дух. Убедитесь, что обе руки выпрямлены.\n4. Поднимите обе гантели в стороны, задействуя дельты, и поднимите их до точки, в которой ваши руки будут параллельны полу. Ни в коем случае не сгибайте локоть во время подъема.\n5. Удерживайте верхнюю позицию на секунду на выдохе и медленно опустите обе руки в исходное положение.\n6. Очень важно выполнять каждое повторение с плавная форма и без использования импульса.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardFBS4, cancellationToken: cancellationToken);
                break;
            case "FBSNext4":
                InlineKeyboardMarkup NextKeyboardFBS5 = Keyboards.GetNextKeyboardFBS5();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 5 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/barbell-curl/barbell-curl-800.jpg"), caption: "1. Начните с пустой 20-килограммовой штанги.\n2. Возьмите штангу ровным хватом снизу (ладони смотрят вперед). Ваши руки должны быть на ширине плеч или немного шире.\n3. Поднимите плечи, задействуйте пресс и сожмите ягодицы.\n4. Сделайте вдох и согните штангу, пока запястья не окажутся немного выше локтей.\n5. Удерживайте верхнюю позицию на секунду, сжимая при этом бицепсы.\n6. Медленно опускайте штангу на выдохе. Полностью выпрямите руки.\n7. Сделайте еще один вдох и снова согните штангу.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardFBS5, cancellationToken: cancellationToken);
                break;
            case "FBSNext5":
                InlineKeyboardMarkup NextKeyboardFBSComplete = Keyboards.GetNextKeyboardFBSComplete();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 6 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/tricep-rope-pushdown/tricep-rope-pushdown-800.jpg"), caption: "1. Выберите подходящий вес, установите блок в самое верхнее положение и прикрепите веревку.\n2. Возьмите веревку обеими руками и разведите локти в стороны.\n3. Сделайте шаг назад и слегка наклоните туловище вперед. \n4. Сделайте вдох и выпрямите руки, сохраняя локти в нужном положении.\n5. Сожмите трицепсы и медленно согните руки, выдыхая при подъеме. Остановитесь, когда ваши запястья окажутся немного выше локтей.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardFBSComplete, cancellationToken: cancellationToken);
                break;
            case "FBSComplete":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Вы завершили тренировку Сплит день 1", cancellationToken: cancellationToken);
                await botClient.SendStickerAsync(callbackQuery.Message.Chat.Id, sticker: InputFile.FromUri("https://i.ibb.co/P9wXmQS/sticker.webp"), replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            ///////////////////
            case "2FBSNext1":
                InlineKeyboardMarkup NextKeyboard2FBSNext2 = Keyboards.GetNextKeyboard2FBS2();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 2 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/02/deadlift-kreuzheben-800x448.png"), caption: "1.Подготовьте штангу и встаньте перед ней.\r\n2.Поставьте ноги под штангу. Когда вы смотрите вниз, должно казаться, что штанга разрезает ваши ступни пополам.\r\n3.Поставьте ступни на ширине плеч, а носки слегка направлены наружу.\r\n4.Наклонитесь и возьмите штангу даже хват сверху. Сохраняйте легкий изгиб в коленях.\r\n5. Сожмите лопатки, чтобы вытянуть грудь и выпрямить спину. \r\n6.Напрягите пресс и сделайте вдох.\r\n7.Потяните штангу по прямой вертикальной линии.\r\n8.Поднимите вес и в верхней точке двигайте бедрами вперед. Не разгибайте поясницу слишком сильно, когда заканчиваете повторение.\r\n9. Опускайте штангу по той же прямой линии, оставаясь в напряжении. Опускаясь вниз, не поддавайтесь желанию согнуть колени, как при обычной становой тяге. Всегда держите их слегка согнутыми.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard2FBSNext2, cancellationToken: cancellationToken);
                break;
            case "2FBSNext2":
                InlineKeyboardMarkup NextKeyboard2FBSNext3 = Keyboards.GetNextKeyboard2FBS3();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 3 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://image.myupchar.com/9494/webp/Lat_Pulldown.webp"), caption: "1. Отрегулируйте наколенник на тренажере так, чтобы он прилегал к бедрам, не оказывая на них слишком сильного давления.\r\n2.Отрегулируйте штифт так, чтобы вы могли с комфортом поднять его, по крайней мере, десять хороших повторений.\r\n3. Встаньте и возьмитесь за ручку хватом сверху, чуть шире плеч.\r\n4.Сядьте и зафиксируйте ноги под подушкой.\r\n5.Вытянув руки и крепко удерживая ручку, поднесите плечи назад и вниз.\r\n6.Вдохните и опустите вес через локти. Думайте о своих руках как о простых крючках для груза – это поможет активировать спину.\r\n7. Когда вы опускаете вес вниз, убедитесь, что ваши локти остаются согнутыми и на одной линии с туловищем; избегайте того, чтобы они расширялись и возвращались назад за ваше тело. \r\n8. Перенесите вес на верхнюю часть груди, задержите сокращение на мгновение и на выдохе выпрямите руки, пока локти не выпрямятся.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard2FBSNext3, cancellationToken: cancellationToken);
                break;
            case "2FBSNext3":
                InlineKeyboardMarkup NextKeyboard2FBSNext4 = Keyboards.GetNextKeyboard2FBS4();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 4 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://images.squarespace-cdn.com/content/v1/5ffcea9416aee143500ea103/1637823947290-H5CTB9ZIB4T1ZID0X6G1/Seated%2BDumbbell%2BShoulder%2BPress.jpeg"), caption: "1. Установите регулируемую спортивную скамью под углом 90 градусов (вертикальная поддержка спины).\n2.Возьмите пару гантелей и сядьте.\n3.Положите гири на бедра.\n4.Отведите плечи назад. , напрягите пресс и сделайте вдох.\n5.Поднимите гантели и поднимите их бедрами.\n6.Расположите гири по бокам.\n7.Сделайте еще один вдох и нажмите гантели вверх и внутрь, постукивая по ним. вверху.\n8.Опускайте гантели, пока локти не окажутся немного ниже плеч, и выдохните.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard2FBSNext4, cancellationToken: cancellationToken);
                break;
            case "2FBSNext4":
                InlineKeyboardMarkup NextKeyboard2FBSNext5 = Keyboards.GetNextKeyboard2FBS5();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 5 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://cdn.shopify.com/s/files/1/1497/9682/files/Close-Grip_Preacher_Curls.jpg?v=1669134373"), caption: "1. Загрузите перекладину и отрегулируйте высоту сиденья скамьи проповедника. Вы должны быть в состоянии положить плечи на подушку и удерживать туловище в вертикальном положении.\n\n2.Расположитесь над тренажером, возьмите штангу ровным хватом снизу (ладони обращены к потолку), поднимите штангу и сядьте. вниз.\n\n3.Отведите плечи назад, задействуйте пресс и вдохните.\n\n4.Поднимите штангу, сгибая бицепсы. Поднимайтесь вверх, пока предплечья не станут почти вертикальными.\n\n5. Медленно вытягивайте руки на выдохе.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard2FBSNext5, cancellationToken: cancellationToken);
                break;
            case "2FBSNext5":
                InlineKeyboardMarkup NextKeyboard2FBSComplete = Keyboards.GetNextKeyboard2FBSComplete();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 6 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://images.squarespace-cdn.com/content/v1/55e406fbe4b0b03c5e7543ae/1495744395987-BD12S3QX4L8L687X9U38/Standing+Two+Arm+Overhead+Dumbbell+Triceps+Extensions"), caption: "1.Возьмите гантель и встаньте прямо.\n2.Поднимите гантель над головой и положите руки на верхнюю блинную платформу ладонями к потолку.\n3.Отведите плечи назад и напрягите пресс.\n4.Сделайте упражнение вдохните и опустите гантель за голову, держа локти по бокам головы.\n5. Сгибайте руки до тех пор, пока не упадете в растяжку, и разгибайте локти на выдохе.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard2FBSComplete, cancellationToken: cancellationToken);
                break;
            case "2FBSComplete":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Вы завершили тренировку Сплит день 2", cancellationToken: cancellationToken);
                await botClient.SendStickerAsync(callbackQuery.Message.Chat.Id, sticker: InputFile.FromUri("https://i.ibb.co/P9wXmQS/sticker.webp"), replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            ////////////////////
            case "3FBSNext1":
                InlineKeyboardMarkup NextKeyboard3FBSNext2 = Keyboards.GetNextKeyboard3FBS2();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 2 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/03/beinpresse.png"), caption: "1.Выберите подходящую нагрузку и сядьте.\n2.Поставьте ноги на платформу на удобной ширине.\n3.Отведите плечи назад, напрягите пресс и возьмитесь за ручки по бокам.\n4.Сделайте вдох. и надавите на платформу вперед, пока не выпрямите ноги.\n5. Задержитесь на мгновение и медленно согните ноги на выдохе", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard3FBSNext2, cancellationToken: cancellationToken);
                break;
            case "3FBSNext2":
                InlineKeyboardMarkup NextKeyboard3FBSNext3 = Keyboards.GetNextKeyboard3FBS3();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 3 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://images.squarespace-cdn.com/content/v1/55e406fbe4b0b03c5e7543ae/1500547685239-22WB8YGJZLO6805RK2TL/Bent+Bar+Seated+Cable+Row"), caption: "1.Выберите подходящую нагрузку и сядьте.\n2.Протянитесь вперед и равномерно возьмитесь за перекладину.\n3.Сядьте назад и поставьте ноги на подножки.\n4.Отведите плечи назад и напрягите пресс.\n5.Напрягитесь вдохните и потяните рукоятку к верхней части живота.\n6. Задержитесь на мгновение и полностью выпрямите руки на выдохе.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard3FBSNext3, cancellationToken: cancellationToken);
                break;
            case "3FBSNext3":
                InlineKeyboardMarkup NextKeyboard3FBSNext4 = Keyboards.GetNextKeyboard3FBS4();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 4 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://i.ibb.co/hFWKLCM/Group-1.png"), caption: "1.Расположите штангу примерно на высоте ключицы на стойке или подставке. Начните с легкого веса, чтобы лучше почувствовать движение, прежде чем добавлять новые блины.\n2. Встаньте на расстоянии нескольких дюймов от перекладины и возьмите ее хватом сверху. Используйте хват немного шире, чем ширина плеч.\n3.Взявшись руками за перекладину, подогните себя под нее, убедитесь, что плечи отведены назад, сделайте вдох, задействуйте ягодицы и толкните ноги, чтобы освободить перекладину.\n4. Сожмите локти, выпрямите спину и посмотрите вперед, сделайте пару шагов назад и поставьте ноги на ширину бедер.\n5. Из этого положения сделайте глубокий вдох, напрягите пресс и надавите на него. локти в вертикальной линии. Не сгибайте и не разгибайте колени, чтобы создать импульс.\n6.Нажимайте до тех пор, пока локти не заблокируются, и на выдохе медленно опускайте штангу до уровня шеи.\n7.Сделайте еще один вдох и снова нажмите вверх.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard3FBSNext4, cancellationToken: cancellationToken);
                break;
            case "3FBSNext4":
                InlineKeyboardMarkup NextKeyboard3FBSNext5 = Keyboards.GetNextKeyboard3FBS5();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 5 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://anabolicaliens.com/cdn/shop/articles/5fa2d13e06ae0ac61604ad32_hammer-curl.png?v=1641753307"), caption: "1.Возьмите пару гантелей, которые позволят вам сделать не менее десяти плавных повторений.\n2.Встаньте во весь рост, выпятите грудь, направьте взгляд вперед и расположите обе гантели по бокам. Руки должны быть прямыми, а ладони обращены к бедрам.\n3.Сделайте вдох и начните выполнять разгибание молота, напрягая бицепсы и удерживая локти в неподвижной плоскости.\n4.Скручивайте гантели до тех пор, пока запястья не окажутся чуть выше локтей. Задержитесь в этом положении на мгновение и выдохните.\n5.Опустите обе гантели одновременно, пока локти не станут прямыми.\n6.Сделайте еще один вдох и повторите.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard3FBSNext5, cancellationToken: cancellationToken);
                break;
            case "3FBSNext5":
                InlineKeyboardMarkup NextKeyboard3FBSComplete = Keyboards.GetNextKeyboard3FBSComplete();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 6 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://adventurefitness.club/wp-content/uploads/2022/11/lying-triceps-extension-vs-skullcrusher.jpeg"), caption: "1.Нагрузите прямую штангу, поднимите ее с пола и держите перед грудью.\n2.Осторожно сядьте на ровную гимнастическую скамью и лягте на спину, держа штангу близко к туловищу.\n3.Вытяните руки и отведите плечи назад. Поставьте ноги на пол.\n4.Сделайте вдох и опустите штангу ко лбу или за голову.\n5.Сделайте паузу и вытяните руки, сохраняя положение локтей. Выдохните в верхней точке.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboard3FBSComplete, cancellationToken: cancellationToken);
                break;
            case "3FBSComplete":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Вы завершили тренировку Сплит день 3", cancellationToken: cancellationToken);
                await botClient.SendStickerAsync(callbackQuery.Message.Chat.Id, sticker: InputFile.FromUri("https://i.ibb.co/P9wXmQS/sticker.webp"), replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            ////////////////////
            case "SplitDay1_1":
                InlineKeyboardMarkup NextKeyboardSplit1_2 = Keyboards.GetNextKeyboardSplitDay1_2();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 2 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://images.squarespace-cdn.com/content/v1/5ffcea9416aee143500ea103/1637823947290-H5CTB9ZIB4T1ZID0X6G1/Seated%2BDumbbell%2BShoulder%2BPress.jpeg"), caption: "1.Установите регулируемую скамью в спортзале под углом 90 градусов (вертикальная опора для спины).\n2.Возьмите пару гантелей и сядьте.\n3.Поместите гантели на верхнюю часть бедер.\n4.Отведите плечи назад, включите пресс и сделайте вдох.\n5.Поднимите гантели и подтолкните их бедрами вверх.\n6.Расположите гантели по бокам.\n7.Сделайте еще один вдох и выжмите гантели вверх и внутрь, постукивая ими по верху.\n8. Опустите гантели, пока локти не окажутся чуть ниже плеч, и выдохните.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit1_2, cancellationToken: cancellationToken);
                break;
            case "SplitDay1_2":
                InlineKeyboardMarkup NextKeyboardSplit1_3 = Keyboards.GetNextKeyboardSplitDay1_3();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 3 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTfOsiNUYbkqPa_Z87lW0dhkOPOn7xrqD5GT9VHd79iEQw2ckVll5mqHUlWOWYPlXIBzp8&usqp=CAU"), caption: "1.Держите в одной руке легкую гантель.Рука должна быть прямой и отведена в сторону.\n2.Отведите плечи назад, направьте взгляд вперед и напрягите мышцы пресса.\n3.Поднимите руку и расположите гантель над головой так, чтобы ладонь была направлена вперед.\n4.Сделайте вдох и медленно опустите гантель, согнув локоть.\n5.Опускайтесь вниз, пока гантель не окажется за головой, и вы не почувствуете растяжение в трицепсе.\n6.Задержитесь на секунду в нижнем положении и полностью разогните локоть, чтобы поднять гантель вверх. Выдохните по пути вверх.\n7.Сделайте еще один вдох и повторите.\n8.Как только вы закончите тренировать один трицепс, возьмите гантель другой рукой и сделайте то же количество повторений.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit1_3, cancellationToken: cancellationToken);
                break;
            case "SplitDay1_3":
                InlineKeyboardMarkup NextKeyboardSplit1_4 = Keyboards.GetNextKeyboardSplitDay1_4();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 4 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/incline-dumbbell-bench-press/incline-dumbbell-bench-press-800.jpg"), caption: "1.Установите наклон скамьи под углом около 45 градусов, возьмите пару гантелей и сядьте.\n2. Взяв по гантели в каждую руку и упираясь в бедра, медленно опуститесь на скамью, одновременно поднимая обе гантели над туловищем. Ваши ноги, попа, верхняя часть спины и голова должны соприкасаться со скамьей.\n3.Поднесите обе гантели к бокам, поставьте ноги, отведите плечи назад и выпятите грудь.Поясница должна быть слегка выгнута, а локти должны быть направлены в стороны, но не разгибаться.\n4.Из этого положения сделайте вдох и поднимите обе гантели вверх к потолку и внутрь до легкого касания.\n5.Выдохните и верните их обратно в стороны.\n6.Продолжайте повторять.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit1_4, cancellationToken: cancellationToken);
                break;
            case "SplitDay1_4":
                InlineKeyboardMarkup NextKeyboardSplit1_5 = Keyboards.GetNextKeyboardSplitDay1_5();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 5 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/dumbbell-lateral-raise/dumbbell-lateral-raise-800.jpg"), caption: "1.Возьмите пару гантелей, которые достаточно легкие, чтобы вы могли сделать не менее двенадцати повторений за сет.\n2.Встаньте во весь рост с гантелями по бокам и ладонями внутрь.\n3.Отведите плечи назад, направьте взгляд вперед и сделайте вдох. Убедитесь, что обе руки прямые.\n4.Поднимите обе гантели в стороны, напрягая дельты, и поднимите их так, чтобы руки были параллельны полу. Не сгибайте руки в локтях во время подъема.\n5.Задержитесь в верхнем положении на секунду на выдохе и медленно опустите обе руки в исходное положение.\n6.Важно выполнять каждое повторение с плавной формой и без использования импульса.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit1_5, cancellationToken: cancellationToken);
                break;
            case "SplitDay1_5":
                InlineKeyboardMarkup NextKeyboardSplit1_Complete = Keyboards.GetNextKeyboardSplitDay1_Complete();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 6 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://static.strengthlevel.com/images/exercises/tricep-rope-pushdown/tricep-rope-pushdown-800.jpg"), caption: "1.Выберите подходящий вес, установите шкив в самое верхнее положение и прикрепите веревку.\n2.Возьмитесь за веревку обеими руками и прижмите локти к бокам.\n3.Сделайте шаг назад и слегка наклоните туловище вперед.\n4.Сделайте вдох и вытяните руки, сохраняя локти в нужном положении.\n5.Сожмите трицепсы и медленно согните руки, выдыхая по пути вверх. Остановитесь, когда запястья окажутся чуть выше локтей.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit1_Complete, cancellationToken: cancellationToken);
                break;
            case "SplitDay1_Complete":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Вы завершили тренировку 3 Сплит день 1", cancellationToken: cancellationToken);
                await botClient.SendStickerAsync(callbackQuery.Message.Chat.Id, sticker: InputFile.FromUri("https://i.ibb.co/P9wXmQS/sticker.webp"), replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            ////////////////////
            case "SplitDay2_1":
                InlineKeyboardMarkup NextKeyboardSplit2_2 = Keyboards.GetNextKeyboardSplitDay2_2();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 2 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://image.myupchar.com/9494/webp/Lat_Pulldown.webp"), caption: "1.Adjust the knee pad on the machine to be right against your thighs without placing too much pressure on them.\r\n2.Adjust the pin to a weight you can comfortably lift for at least ten good repetitions.\r\n3.Stand up, and grab the handle with a slightly wider than shoulder-width overhand grip.\r\n4.Sit down and secure your legs underneath the pad.\r\n1.Настройте коленный вкладыш на тренажере так, чтобы он прилегал к вашим бедрам, не оказывая на них слишком сильного давления.\n2.Установите штырь на вес, который вы можете комфортно поднять как минимум на десять хороших повторений.\n3.Встаньте и возьмитесь за рукоятку хватом чуть шире ширины плеч.\n4.Сядьте и закрепите ноги под вкладышем.\n5.Вытянув руки и крепко держа рукоятку, отведите плечи назад и вниз.\n6.Сделайте вдох и потяните вес вниз через локти. Считайте, что ваши руки - это просто крюки для веса - это поможет активизировать спину.\n7.Когда вы тянете вес вниз, следите за тем, чтобы локти оставались подтянутыми и находились на одной линии с туловищем; не допускайте их разгибания и отведения за спину.\n8.Подтяните вес к верхней части груди, задержите сокращение на мгновение и, выдыхая, вытяните руки до прямых локтей.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit2_2, cancellationToken: cancellationToken);
                break;
            case "SplitDay2_2":
                InlineKeyboardMarkup NextKeyboardSplit2_3 = Keyboards.GetNextKeyboardSplitDay2_3();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 3 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://images.squarespace-cdn.com/content/v1/5ffcea9416aee143500ea103/1638178897186-6VBYF3AB4T76O6GMG7M5/Standing%2BBiceps%2BCable%2BCurl.png"), caption: "1.Прикрепите прямую штангу к низкому тросовому шкиву.\n2.Установите на весовом стеке соответствующую нагрузку.\n3.Присядьте на корточки и возьмите прямую штангу ровно, ладонями вверх.\n4.Вытяните тело и сделайте шаг назад (если необходимо), чтобы поднять вес со стека.\n5. Отведите плечи назад, напрягите пресс и прижмите локти к бокам.\n6.Сделайте вдох и скрутите гирю, держа локти ровно.\n7.Поднимайте гирю так, чтобы запястья были чуть выше локтей.\n8.Задержитесь в верхнем положении на мгновение и медленно опустите гирю, выдыхая по пути вниз.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit2_3, cancellationToken: cancellationToken);
                break;
            case "SplitDay2_3":
                InlineKeyboardMarkup NextKeyboardSplit2_4 = Keyboards.GetNextKeyboardSplitDay2_4();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 4 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/03/hyperextension.png"), caption: "1.Встаньте на тренажер для разгибания спины. Упритесь бедрами в подушечку, а лодыжками в скобы для ног.\n2. Включите пресс, скрестите руки перед грудью и сделайте вдох.\n3. Опускайтесь, сгибая бедра, и двигайтесь вниз, пока не почувствуете растяжение в подколенных сухожилиях и ягодицах.\n4. Включите ягодицы и поясницу, чтобы поднять туловище в верхнее положение, сделайте паузу и выдохните.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit2_4, cancellationToken: cancellationToken);
                break;
            case "SplitDay2_4":
                InlineKeyboardMarkup NextKeyboardSplit2_5 = Keyboards.GetNextKeyboardSplitDay2_5();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 5 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/02/rudern-geraet.png"), caption: "1.Выберите подходящий груз.\n2.Сядьте и возьмитесь за рукоятки нейтральным хватом (руки обращены друг к другу)\n3.Отведите плечи назад и напрягите пресс.\n4.Сделайте вдох и одновременно потяните рукоятки.\n5.Сожмите спину и задержитесь на мгновение.\n6.Медленно вытяните руки и выдохните.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit2_5, cancellationToken: cancellationToken);
                break;
            case "SplitDay2_5":
                InlineKeyboardMarkup NextKeyboardSplit2_Complete = Keyboards.GetNextKeyboardSplitDay2_Complete();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 6 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://anabolicaliens.com/cdn/shop/articles/5fa2d13e06ae0ac61604ad32_hammer-curl.png?v=1641753307"), caption: "1.Возьмите пару гантелей, которые позволят вам сделать не менее десяти плавных повторений.\r\n2.Встаньте во весь рост, выпятите грудь, направьте взгляд вперед и расположите обе гантели по бокам. Руки должны быть прямыми, а ладони обращены к бедрам.\r\n3. Сделайте вдох и начните разгибание молота, напрягая бицепсы и удерживая локти в неподвижной плоскости.\r\n4. Разгибайте гантели до тех пор, пока ваши запястья не окажутся чуть выше локтей. Задержитесь на мгновение и выдохните.\r\n5.Опустите обе гантели одновременно, пока локти не станут прямыми.\r\n6.Сделайте еще один вдох и повторите.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit2_Complete, cancellationToken: cancellationToken);
                break;
            case "SplitDay2_Complete":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Вы завершили тренировку Сплит день 2", cancellationToken: cancellationToken);
                await botClient.SendStickerAsync(callbackQuery.Message.Chat.Id, sticker: InputFile.FromUri("https://i.ibb.co/P9wXmQS/sticker.webp"), replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            ////////////////////
            case "SplitDay3_1":
                InlineKeyboardMarkup NextKeyboardSplit3_2 = Keyboards.GetNextKeyboardSplitDay3_2();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 2 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://anabolicaliens.com/cdn/shop/articles/5fd7d1877ec308ebe3f92223_seated-hamstring-curl.png?v=1641744558"), caption: "1.Выберите подходящий вес.\n2.Отрегулируйте положение колодки. Она должна быть напротив ваших ахилловых пяток.\n3.Сядьте, поставьте ноги на подушку и возьмитесь за ручки по бокам.\n4.Отведите плечи назад, напрягите пресс и вдохните.\n5.Согните колени, сокращая подколенные сухожилия.\n6.Задержитесь на мгновение и медленно разведите колени на выдохе.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit3_2, cancellationToken: cancellationToken);
                break;
            case "SplitDay3_2":
                InlineKeyboardMarkup NextKeyboardSplit3_3 = Keyboards.GetNextKeyboardSplitDay3_3();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 3 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2020/03/beinstrecken-geraet-1.png"), caption: "1.Выберите подходящий вес.\n2.Отрегулируйте площадку так, чтобы она прилегала к голеням, чуть выше стоп в положении сидя.\n3.Сядьте, возьмитесь за ручки по бокам, прижмите голени к площадке и отведите плечи.\n4.Сделайте вдох и выпрямите ноги, задействовав квадрицепсы.\n5.Поднимайте вес, пока колени не выпрямятся.\n6.Задержитесь на мгновение и медленно согните колени на выдохе.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit3_3, cancellationToken: cancellationToken);
                break;
            case "SplitDay3_3":
                InlineKeyboardMarkup NextKeyboardSplit3_4 = Keyboards.GetNextKeyboardSplitDay3_4();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 4 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://kinxlearning.com/cdn/shop/files/exercise-3_1000x.jpg?v=1613154659"), caption: "1.\n1Загрузите тренажер, сядьте и отрегулируйте площадку. Ваши бедра должны плотно прилегать к колодке, что позволит вам снять вес, раздвинув лодыжки.\n2.Прижмите бедра к колодке и крепко возьмитесь за ручки.\n3.Сделайте вдох и снимите вес, разгибая икры.\n4.Отведите страховочную штангу в сторону и разгибайте лодыжки, чтобы опустить вес, пока не почувствуете растяжение в икрах.\n5.Нажмите на стопы и разгибайте икры, выдыхая в верхней точке.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit3_4, cancellationToken: cancellationToken);
                break;
            case "SplitDay3_4":
                InlineKeyboardMarkup NextKeyboardSplit3_5 = Keyboards.GetNextKeyboardSplitDay3_5();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 5 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://fitnessprogramer.com/wp-content/uploads/2021/02/plank.gif"), caption: "1. Лягте на пол.\n2. Вытяните свое тело. Опирайтесь нижней частью тела на пальцы ног, а верхней - на предплечья.\n3.Держите плечи нейтральными, а пресс задействованным.\n4.Задержитесь в этом положении как можно дольше, дыша ровно.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit3_5, cancellationToken: cancellationToken);
                break;
            case "SplitDay3_5":
                InlineKeyboardMarkup NextKeyboardSplit3_Complete = Keyboards.GetNextKeyboardSplitDay3_Complete();
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Переходим к 6 упражнению", cancellationToken: cancellationToken);
                await botClient.SendPhotoAsync(callbackQuery.Message.Chat.Id, photo: InputFile.FromUri("https://training.fit/wp-content/uploads/2019/08/crunches-liegend-800x448.png"), caption: "1.Лягте на пол, согните колени и поставьте стопы ровно на пол.\n2.Поднимите руки вверх и заведите пальцы за голову.\n3.Включите пресс и сделайте вдох.\n4. Согните туловище, поднимая лопатки на несколько дюймов от пола.\n5.Медленно опуститесь и выдохните.", cancellationToken: cancellationToken);
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "По завершению упражнения нажмите на кнопку", replyMarkup: NextKeyboardSplit3_Complete, cancellationToken: cancellationToken);
                break;
            case "SplitDay3_Complete":
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Вы завершили тренировку Сплит день 3", cancellationToken: cancellationToken);
                await botClient.SendStickerAsync(callbackQuery.Message.Chat.Id, sticker: InputFile.FromUri("https://i.ibb.co/P9wXmQS/sticker.webp"), replyMarkup: general_keyboard, cancellationToken: cancellationToken);
                break;
            default:
                Console.WriteLine("Ошибка встроенной кнопки");
                break;
        }
    }
    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error occurred: {error.Message}");
        return Task.CompletedTask;
    }
}