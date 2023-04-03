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
using System.IO;
using System.Text;
using static System.Net.WebRequestMethods;

static DataTable Select(string selectSQL)
{
    DataTable dataTable = new("dataBase");
    SqlConnection sqlConnection = new("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
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
client.StartReceiving(updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token);
var me = await client.GetMeAsync();
string path = @"C:\Users\Администратор\Desktop\zapis'_bot.txt";
if (System.IO.File.Exists(path))
{
    Console.WriteLine("файл есть");
}
else
{
    Console.WriteLine("файла нет");
}
Console.WriteLine($"Старт @{me.Username}, время {DateTime.Now}");
Console.ReadLine();
cts.Cancel();
async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
{
    InlineKeyboardMarkup personalArea = new(new[]
    {
        /* new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Сменить пароль",callbackData:"personalAreaPasswordUpdate")//смена пароля слушателя
        },*/
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Выход",callbackData:"personalAreaBack")//назад в кабинете слушателя
        }
    });
    InlineKeyboardMarkup groupvk= new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"groupvkback")
        }
    });
    InlineKeyboardMarkup phone_back = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"phoneback")
        }
    });
    InlineKeyboardMarkup dopback = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"dopback")
        }
    });
    InlineKeyboardMarkup doporprof = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Тестирование и контроль качества ПО",callbackData:"doporprof1")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Языки программирования",callbackData:"doporprof2")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Сетевое и системное администрирование",callbackData:"doporprof3")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Информационная безопасность",callbackData:"doporprof4")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Сенсорика,электроника и радиотехника",callbackData:"doporprof5")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Образование",callbackData:"doporprof6")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Экономика и управление",callbackData:"doporprof7")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"doporprofback")
        }
    });
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
            InlineKeyboardButton.WithCallbackData(text:"Направления для обучения",callbackData:"myCommand2")// тестирование там и тд
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Специалисты",callbackData:"myCommand6")// тестирование там и тд
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Группа ВК",callbackData:"myCommand5")//группа вк
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
    InlineKeyboardMarkup menuback = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text:"Назад",callbackData:"menuback1")//кнопка назад
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
        if (!words.Contains("/start") && words.Length == 2)
        {
            string authorization1 = words[0];
            string authorization2 = words[1];
            Console.WriteLine("Сплит слов для авторизации: " + authorization1 + " " + authorization2);
            DataTable sel = Select("select * from [dbo].[Listeners] where login_listener = '" + authorization1 + "' and password_listener = '" + authorization2 + "'");
            if (sel.Rows.Count > 0)//личный кабинет слушателя
            {
                string path = @"C:\Users\Администратор\Desktop\zapis'_bot.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteLineAsync($"Авторизовался пользователь: логин - {authorization1}, пароль - {authorization2}");
                }
                string connectionString = @"server=localhost\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;";
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
        else if (words.Length != 2 && words.Length !=5)
        {
            await client.SendTextMessageAsync(chatId, "Что-то пошло не так, введите /start для начала работы", cancellationToken: token);
        }
        else if (words.Length == 5)
        {
            if (words[3].Contains("+") && words[4].Contains("@"))
            {
                string path = @"C:\Users\Администратор\Desktop\zapis'_bot.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    await writer.WriteLineAsync($"\nПодана заявка на обучение:\nФИО - {words[0]} {words[1]} {words[2]}\nТелефон - {words[3]}\nПочта - {words[4]}");
                }
                await client.SendTextMessageAsync(chatId, "Ваша заявка на обучение принята, с вами скоро свяжутся.", replyMarkup: phone_back, cancellationToken: token);
                await client.DeleteMessageAsync(chatId,update.Message.MessageId-1,cancellationToken:token);
            }
            else
            {
                await client.SendTextMessageAsync(chatId,"Вы ввели некорректно почту или телефон, проверьте правильность ввода и повторите попытку.",cancellationToken: token);
            }
        }
    }
    switch (update.Type)
    {
        case UpdateType.CallbackQuery:
        {
                if (update.CallbackQuery.Data == "ApplicationForTraining")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Введите ваше ФИО, номер телефона (в формате +7...), электронную почту.\n" +
                    "Ввод осуществляется в одно сообщение через пробел",
                    replyMarkup: groupvk,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof1")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Тестирование и контроль качества ПО\nПрофессиональная переподготовка:\n" +
                    "\n•Тестирование и контроль качества ПО (https://do.tusur.ru/?45865)\n" +
                    "•Специалист по тестированию ПО (https://do.tusur.ru/?45856)\n" +
                    "\nПовышение квалификации:\n" +
                    "\n•Автоматизированное тестирование ПО (https://do.tusur.ru/?45714)\n" +
                    "•Тестирование и контроль качества ПО (https://do.tusur.ru/software_quality_assurance)\n" +
                    "•Модульное тестирование ПО (https://do.tusur.ru/?45696)\n" +
                    "•Тестирование мобильных приложений (https://do.tusur.ru/?45713)\n" +
                    "\nБольше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof2")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Языки программирования\nПовышение квалификации:\n" +
                    "\n•Front-end разработка (https://do.tusur.ru/?45568)\n" +
                    "•Программирование на языке Python (https://do.tusur.ru/?45571)\n" +
                    "•Разработка нейронных сетей на Python с нуля (https://do.tusur.ru/?45849)\n" +
                    "•Разработка нейронных сетей на Python (https://do.tusur.ru/?45850)\n" +
                    "•Основы Java-программирования (https://do.tusur.ru/?45818)\n" +
                    "•Основы SQL (https://do.tusur.ru/?45852)\n" +
                    "•Программирование на языке Java. Базовый курс (https://do.tusur.ru/courses/programs/java)\n" +
                    "•Профессиональная разработка на языке Java (https://do.tusur.ru/JavaDeveloper)\n" +
                    "•Объектно-ориентированное программирование (https://do.tusur.ru/courses/programs/oop)\n" +
                    "•Объектно-ориентированное программирование на языке С++ (https://do.tusur.ru/?45573)\n" +
                    "•Конфигурирование и программирование в системе «1С:Предприятие» (https://do.tusur.ru/courses/1c-conf-and-prog)\n" +
                    "\nБольше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof3")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Сетевое и системное администрирование\nПрофессиональная переподготовка:\n" +
                    "\n•Системное администрирование (https://do.tusur.ru/?45834)\n" +
                    "•Проектирование, строительство и эксплуатация инфокоммуникационных сетей (https://do.tusur.ru/?45833)\n" +
                    "\nПовышение квалификации:\n" +
                    "\n•Сетевой специалист (https://do.tusur.ru/?45832)\n" +
                    "•Сетевой техник (https://do.tusur.ru/?45817)\n" +
                    "•Построение масштабируемых и распределенных сетей (https://do.tusur.ru/courses/icnd2)\n" +
                    "•Введение в сетевые технологии (https://do.tusur.ru/?45831)\n" +
                    "•Основы маршрутизации и коммутации (https://do.tusur.ru/courses/programs/Routing-and-Switching-Essentials)\n" +
                    "•Построение масштабируемых сетей (https://do.tusur.ru/courses/programs/Scaling-Networks)\n" +
                    "•Построение распределенных сетей (https://do.tusur.ru/courses/programs/Connecting-Networks)\n" +
                    "•Администрирование Linux (https://do.tusur.ru/courses/programs/linux)\n" +
                    "•Система мониторинга Zabbix (https://do.tusur.ru/zabbix-monitoring-software)\n" +
                    "•Установка, настройка и администрирование Microsoft Windows Server (https://do.tusur.ru/courses/programs/ms-windows-server)\n" +
                    "\nБольше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof4")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Информационная безопасность\nПовышение квалификации:\n" +
                    "\n•Обеспечение безопасности персональных данных при их обработке в информационных системах персональных данных (https://do.tusur.ru/courses/programs/personal_data_protection)\n" +
                    "•Операции с сетевыми данными в области информационной безопасности (https://do.tusur.ru/?45822)\n" +
                    "•Цифровые технологии информационной безопасности (https://do.tusur.ru/?45823)\n" +
                    "\nБольше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof5")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Сенсорика, электроника и радиотехника\nПовышение квалификации:\n" +
                    "\n•Синтез частот и характеризация источников сигналов на СВЧ в сенсорных системах (https://do.tusur.ru/?45853)\n" +
                    "•Основы синтеза и моделирования цифровых схем на ПЛИС (https://do.tusur.ru/?45835)\n" +
                    "•Основы конструирования радиоэлектронных средств (https://do.tusur.ru/?45838)\n" +
                    "•Современные подходы к автоматизации процесса измерения (https://do.tusur.ru/?45821)\n" +
                    "•Современные методы программной обработки сигналов сенсоров различных типов (https://do.tusur.ru/?45792)\n" +
                    "•Медицинские информационные системы (https://do.tusur.ru/courses/programs/medical_information_systems)\n" +
                    "•Веб-интерфейсы и специальные сетевые протоколы для сбора информации и управления сенсорными системами (https://do.tusur.ru/courses/programs/web_interfaces_and_network_protocols)\n" +
                    "•Активные и пассивные элементы СВЧ интегральной наноэлектроники (https://do.tusur.ru/courses/programs/elements_of_SHF_integrated_nanoelectronics)\n" +
                    "•Разработка элементов и узлов трактов приёма и передачи твердотельных СВЧ сенсорных модулей (https://do.tusur.ru/courses/programs/SHF-modules)\n" +
                    "\n•Программирование логических интегральных схем. Реализация алгоритмов физического уровня передатчика системы беспроводной связи (https://do.tusur.ru/?45842)\n" +
                    "•Разработка встраиваемых систем для систем интернета вещей. Введение (https://do.tusur.ru/?45843)\n" +
                    "•Искусственный интеллект в задачах биометрической аутентификации для обеспечения доверенного взаимодействия (https://do.tusur.ru/?45847)\n" +
                    "\nЭто неполный список направлений. Больше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof6")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Образование\nПрофессиональная переподготовка:\n" +
                    "\n•Преподаватель высшей школы в эпоху цифровизации (https://do.tusur.ru/courses/programs/university-professor)\n" +
                    "\nПовышение квалификации:\n" +
                    "•Система ДПО организации: от анализа рынка до программы развития (https://do.tusur.ru/courses/programs/online-school-dpo-2)\n" +
                    "•Проектирование программы ДПО: от анализа рынка до визитки курса (https://do.tusur.ru/courses/programs/online-school-dpo-1)\n" +
                    "•Визуализация учебных материалов (https://do.tusur.ru/?45657)\n" +
                    "•Интерактивность как фактор мотивации обучения (https://do.tusur.ru/?45682)\n" +
                    "•Интерактивные методы обучения и элементы геймификации в электронном курсе (https://do.tusur.ru/?45658)\n" +
                    "•Мобильные приложения для преподавателя (https://do.tusur.ru/?45659)\n" +
                    "•Методы искусственного интеллекта и обработки данных в реализации персонализированного обучения (https://do.tusur.ru/?45837)\n" +
                    "•Модели и технологии интеграции готовых онлайн-курсов в учебный процесс (https://do.tusur.ru/?45820)\n" +
                    "•Мотивация и вовлечение в электронном обучении (https://do.tusur.ru/?45776)\n" +
                    "•Проектирование и разработка электронных курсов в цифровой образовательной среде. Базовый уровень (https://do.tusur.ru/?45769)\n" +
                    "•Проектирование и разработка электронных курсов в цифровой образовательной среде. Продвинутый уровень (https://do.tusur.ru/?45805)\n" +
                    "•Проектирование учебного процесса по дисциплине в цифровой образовательной среде (https://do.tusur.ru/?45806)\n" +
                    "•Разработка и использование электронных курсов в учебном процессе (https://do.tusur.ru/?45766)\n" +
                    "•Системный администратор Moodle (https://do.tusur.ru/?45807)\n" +
                    "\nБольше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprof7")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Экономика и управление\nПрофессиональная переподготовка:\n" +
                    "\n•Государственное и муниципальное управление (https://do.tusur.ru/?45864)\n" +
                    "•Кадровое делопроизводство и работа с персоналом (https://do.tusur.ru/?45717)\n" +
                    "•Организация и документальное обеспечение управления организацией (https://do.tusur.ru/?45739)\n" +
                    "•Управление персоналом (https://do.tusur.ru/courses/programs/personnel-management)\n" +
                    "•Управление персоналом и подразделением организации (https://do.tusur.ru/?45718)\n" +
                    "•Управление проектами (https://do.tusur.ru/courses/programs/project-management )\n" +
                    "•Экономика предприятия (https://do.tusur.ru/?45640)\n" +
                    "\nПовышение квалификации:\n" +
                    "\n•Работа в MS Excel (https://do.tusur.ru/?45684)\n" +
                    "•Работа в MS Excel: расширенные возможности https://do.tusur.ru/?45685()\n" +
                    "•Excel для экономистов (https://do.tusur.ru/?45866)\n" +
                    "•Excel для бухгалтеров, экономистов, финансовых менеджеров (https://do.tusur.ru/?45867)\n" +
                    "\n•Управление персоналом (https://do.tusur.ru/?45879)\n" +
                    "•Управление проектами (https://do.tusur.ru/?45880)\n" +
                    "•Управление государственными и муниципальными закупками (https://do.tusur.ru/?45881)\n" +
                    "•Государственная и муниципальная служба (https://do.tusur.ru/?45882)\n" +
                    "•Государственные и муниципальные финансы (https://do.tusur.ru/?45883)\n" +
                    "\n•Сервис-менеджер. Клиентский сервис от А до Я (https://do.tusur.ru/?45855)\n" +
                    "•Agile, Scrum – гибкие методологии управления проектами (https://do.tusur.ru/?45700)\n" +
                    "•Интернет-маркетинг (https://do.tusur.ru/?45587)\n" +
                    "•Управление проектами: методология, подходы, цифровые инструменты (https://do.tusur.ru/courses/programs/project_management)\n" +
                    "•Управление финансами и основы инвестирования (https://do.tusur.ru/courses/programs/personal_finance_management)\n" +
                    "•Системный подход в управлении компанией, сотрудниками и взаимоотношениями с клиентами с помощью \"Битрикс24\" (https://do.tusur.ru/?45841)\n" +
                    "\nБольше информации - https://do.tusur.ru/",
                    replyMarkup: dopback,
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
                    text: "Здесь вы можете узнать следующую информацию:",
                    replyMarkup: menu0, 
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommand6")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Специалист по вопросам поступления\nБезрукова Вера Петровна\nКонтактные данные:\nПочта - bvp@2i.tusur.ru\nТелефон - 8 (3822) 701-736, 8-923-407-66-83\nСкайп - bezrukova_vera\nWhatsApp - 8-923-434-11-90\n" +
                    "\nСпециалист по вопросам обучения школьников\nТахтабаева Ильмира Хусаиновна\nКонтактные данные:\nПочта - tih@2i.tusur.ru\nТелефон - 8 (3822) 533-077\nWhatsApp - 8-923-443-24-16\n" +
                    "\nКуратор дистанционного обучения\nЗавадовская Олеся Александровна\nКонтактные данные:\nПочта - zoa@2i.tusur.ru\nТелефон - 8 (3822) 701-736",
                    replyMarkup: phone_back,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommand1")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Номер телефона справочной: Тел. +7 (3822) 70-17-36",
                    replyMarkup: phone_back,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommand5")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Ссылка на группу: https://vk.com/cit_tusur_ru",
                    replyMarkup: groupvk,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "myCommand2")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Направление для обучения:",
                    replyMarkup: doporprof,
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
                if (update.CallbackQuery.Data == "phoneback")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здесь вы можете узнать следующую информацию:",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "groupvkback")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здесь вы можете узнать следующую информацию:",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "doporprofback")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Здесь вы можете узнать следующую информацию:",
                    replyMarkup: menu0,
                    cancellationToken: token);
                }
                if (update.CallbackQuery.Data == "dopback")
                {
                    Message sentMessage = await client.EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: "Направление для обучения:",
                    replyMarkup: doporprof,
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
