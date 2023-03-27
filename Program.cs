using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;

static DataTable Select(string selectSQL)
{
    DataTable dataTable = new("dataBase");
    SqlConnection sqlConnection = new("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
    sqlConnection.Open();
    SqlCommand sqlCommand = sqlConnection.CreateCommand();
    sqlCommand.CommandText = selectSQL;
    SqlDataAdapter sqlDataAdapter = new(sqlCommand);
    sqlDataAdapter.Fill(dataTable);
    sqlConnection.Close();
    return dataTable;
}
var client = new TelegramBotClient("5955487728:AAG44mfbhOpcSFV4Howjno5I94QSqHzaYcI");
using var cts = new CancellationTokenSource();
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};
//Console.WriteLine("Запущен бот " + client.GetMeAsync().Result.FirstName);
//client.AnswerCallbackQueryAsync += CallbackQueryArg;
client.StartReceiving(updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token);
var me = await client.GetMeAsync();
Console.WriteLine($"Старт @{me.Username}");
Console.ReadLine();
cts.Cancel();
async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
{
    InlineKeyboardMarkup personalArea = new(new[]
    {
         new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Сменить пароль",callbackData:"personalAreaPasswordUpdate")//смена пароля слушателя
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Выход",callbackData:"personalAreaBack")//назад в кабинете слушателя
        }
    });
    /*InlineKeyboardMarkup choice = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Да",callbackData:"choiceYes")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Нет",callbackData:"choiceNo")
        }
    });*/
    InlineKeyboardMarkup menu0 = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Подать заявку на обучение",callbackData:"ApplicationForTraining")//заявка на обученеи
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Номер телефона справочной",callbackData:"myCommand1")//номер телефона справочной
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Адреса университета 1",callbackData:"myCommand2")//надо убрать
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Адреса университета 2",callbackData:"myCommand5")//надо убрать
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Адреса общежитий",callbackData:"myCommand6")//надо убрать
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Специалисты по вопросам поступления",callbackData:"myCommand3")//спецы
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Образование/повышение квалификации",callbackData:"myCommand4")//компетенции
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"myCommandBack")//кнопка назад
        }
    });
    InlineKeyboardMarkup menu1 = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Слушатель",callbackData:"callback1")//переход к логину/паролю
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Обычный пользователь",callbackData:"callback2")//переход в функционал поддержки
        }
    }) ;
    InlineKeyboardMarkup menuauth = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"callbackbackuser")//кнопка назад
        }
    });
    InlineKeyboardMarkup menuadmin = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Войти",callbackData:"callbackauthadmin")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"callbackbackadmin")
        }
    });
    InlineKeyboardMarkup menuback = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"menuback1")//кнопка назад
        }
    });
    InlineKeyboardMarkup passwordupdate = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"menuback1")//кнопка назад
        }
    });
    InlineKeyboardMarkup menuback1 = new(new[]
   {
        new []
        {
            InlineKeyboardButton.WithUrl(text:"Общежитие №1",url:"https://go.2gis.com/b6yq8")
        },
        new []
        {
            InlineKeyboardButton.WithUrl(text:"Общежитие №2",url:"https://go.2gis.com/ku65t")
        },
        new []
        {
            InlineKeyboardButton.WithUrl(text:"Общежитие №3",url:"https://go.2gis.com/eb3xx")
        },
        new []
        {
            InlineKeyboardButton.WithUrl(text:"Общежитие №4",url:"https://go.2gis.com/rsg15")
        },
        new []
        {
            InlineKeyboardButton.WithUrl(text:"Общежитие №5",url:"https://go.2gis.com/gqsiq")
        },
        new []
        {
            InlineKeyboardButton.WithUrl(text:"Общежитие №6",url:"https://go.2gis.com/5bsmu")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"menuback11")
        }
    });
    if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
    {
        var chatId = update.Message.Chat.Id;
        var messageText = update.Message.Text;
        string str = update.Message.From.FirstName;
        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        string text = messageText;
        string[] words = text.Split(' ');
        if (!words.Contains("/start") && !words.Contains("/adminprofile") && words.Length == 2)
        {
            string authorization1 = words[0];
            string authorization2 = words[1];
            Console.WriteLine("Сплит слов для авторизации: " + authorization1 + " " + authorization2);
            DataTable sel = Select("select * from [dbo].[Listeners] where login_listener = '" + authorization1 + "' and password_listener = '" + authorization2 + "'");
            if (sel.Rows.Count > 0)//личный кабинет слушателя
            {
                string connectionString = @"server=WIN-NHF22QP2E4K\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;";
                string sqlExperssion = "select Persons.surname,Persons.name,Persons.patronymic,Competence.name_competce,DAY(Courses.date_start),MONTH(Courses.date_start),YEAR(Courses.date_start),DAY(Courses.date_end),MONTH(Courses.date_end),YEAR(Courses.date_end),timetable.[day],timetable.time_1,timetable.time_2,Listeners.email,sex.sex_name from Listeners,Persons,Competence,Courses,timetable,sex where Courses.id_time = timetable.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id and Listeners.id_person = Persons.id and Persons.id_sex = sex.id and login_listener = '" + words[0] + "'";
                using SqlConnection connection = new SqlConnection(connectionString);
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        SqlCommand command = new(sqlExperssion, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            object surname = reader.GetValue(0);
                            object name = reader.GetValue(1);
                            object patronymic = reader.GetValue(2);
                            object competence = reader.GetValue(3);
                            object datestartday = reader.GetValue(4);
                            object datestartmounth = reader.GetValue(5);
                            object datestartYear = reader.GetValue(6);
                            object dateendday = reader.GetValue(7);
                            object dateendmounth = reader.GetValue(8);
                            object dateendyear = reader.GetValue(9);
                            object timeday = reader.GetValue(10);
                            object time1 = reader.GetValue(11);
                            object time2 = reader.GetValue(12);
                            object email = reader.GetValue(13);
                            object sex = reader.GetValue(14);
                            await client.SendTextMessageAsync(
                                chatId,
                                text: "Личный кабинет пользователя\nФИО:" +
                                " " + surname.ToString() + " "
                                + name.ToString() + " "
                                + patronymic.ToString() + "\nПол: " + sex.ToString() + 
                                "\nКомпетенция: " + competence.ToString() +
                                "\nДата начала курса: " + datestartday.ToString() + "." + datestartmounth.ToString() + "." + datestartYear.ToString() + 
                                "\nДата окончания курса: " + dateendday.ToString() + "." + dateendmounth.ToString() + "." + dateendyear.ToString() + 
                                "\nПроведение занятий: " + timeday.ToString() + " c " + time1.ToString()[..+5] + " до " + time2.ToString()[..+5] +
                                "\nПочта: " + email.ToString(),
                                replyMarkup: personalArea,
                                cancellationToken: token);
                        }
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            else if (sel.Rows.Count == 0)
            {
                await client.SendTextMessageAsync(chatId, "Вход не выполнен, попробуйте снова", cancellationToken: token);
            }
            else
            {
                await client.SendTextMessageAsync(chatId, "Что-то пошло не так, введите /start для начала работы", cancellationToken: token);
            }
        }
        else if (messageText == "/start")
        {
            await client.SendTextMessageAsync(chatId, $"Здравствуйте, {str} ,выберите, вы слушатель или пользователь:", replyMarkup: menu1, cancellationToken: token);
        }
        else if (words.Length != 2)
        {
            await client.SendTextMessageAsync(chatId, "Что-то пошло не так, введите /start для начала работы", cancellationToken: token);
        }
    }
    switch (update.Type)
    {
        case UpdateType.CallbackQuery:
        {
                if (update.CallbackQuery.Data == "personalAreaPasswordUpdate")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Для смены пароля выполните следующие действия:\n1)Введите логин и новый пароль в одно сообщение через пробел\n",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "personalAreaBack")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здравствуйте, выберите, вы слушатель или пользователь:",
                    replyMarkup: menu1,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "callback1")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Вход в личный кабинет слушателя\nВведите Логин и Пароль\n(в одно сообщение через пробел)",
                    replyMarkup: menuauth,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "callback2")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здесь вы можете узнать информацию о следующем",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "callbackbackuser")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здравствуйте, выберите, вы слушатель или пользователь:",
                    replyMarkup: menu1,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommandBack")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здравствуйте, выберите, вы слушатель или пользователь:",
                    replyMarkup: menu1,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommand1")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Номер телефона справочной: Тел. +7 (3822) 70-17-36",
                    replyMarkup: menuback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "menuback1")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здесь вы можете узнать информацию о следующем",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "menuback11")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здесь вы можете узнать информацию о следующем",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommand6")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Адреса общежитий:\n19 Гвардейской Дивизии, 9а (Общежитие №1)\r\nВершинина, 32 (Общежитие №2, Центр Кибербезопасность)\r\nПроспект Кирова, 56а (Общежитие №3)\r\nФедора Лыткина, 10 (Общежитие №4)\r\nФедора Лыткина, 18 (Общежитие №5,Дирекция студ. городка)\r\nФедора Лыткина, 8 (Общежитие №6)",
                    replyMarkup: menuback1,
                    cancellationToken: token);
                }
                Console.WriteLine($"Received a '{update.CallbackQuery.Data}' button in chat.");
                break;
        }
        default:
            break;
    }
}
Task HandlePollingErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
{
    var ErrorMessage = arg2 switch
    {
        ApiRequestException apiRequestException
        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => arg2.ToString()
    };
    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
