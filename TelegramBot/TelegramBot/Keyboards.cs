using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Keyboards
{
    public static class Keyboards
    {
        public static ReplyKeyboardMarkup GetGeneralKeyboard()
        {
            return new(new[]
            {
            new KeyboardButton[] { "Библиотека Упражнений 📚"},
            new KeyboardButton[] { "Программы тренировок 💪" },
            new KeyboardButton[] {"Полезные Статьи 📰" }
        })
            {
                ResizeKeyboard = true,
            };
        }
        public static ReplyKeyboardMarkup GetMuscleGroupKeyboard()
        {
            return new(new[]
            {
            new KeyboardButton[] { "Грудные" },
            new KeyboardButton[] { "Бицепс", "Трицепс" },
            new KeyboardButton[] { "В главное меню" },
        })
            {
                ResizeKeyboard = true
            };
        }
        public static InlineKeyboardMarkup GetHowToBPKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Подробнее", "howto_bp")
            },
        });
        }
        public static InlineKeyboardMarkup GetHowToCFKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Подробнее", "howto_cf")
            },
        });
        }
        public static InlineKeyboardMarkup GetHowToBCKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Подробнее", "howto_bc")
            },
        });
        }
        public static InlineKeyboardMarkup GetHowToPCKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Подробнее", "howto_pc")
            },
        });
        }
        public static InlineKeyboardMarkup GetHowToTPKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Подробнее", "howto_tp")
            },
        });
        }
        public static InlineKeyboardMarkup GetHowToSCKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Подробнее", "howto_sc")
            },
        });
        }
        /// ///////////////////////////////////////////////////////////
        public static ReplyKeyboardMarkup GetTrainingProgramsKeyboard()
        {
            return new(new[]
            {
            new KeyboardButton[]{"3 дня Фулл Боди"},
            new KeyboardButton[]{"3 дня Сплит"},
            new KeyboardButton[]{"В главное меню"}
        })
            {
                ResizeKeyboard = true,
            };
        }
        public static InlineKeyboardMarkup GetNextKeyboardFBS1()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "FBSNext1")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardFBS2()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "FBSNext2")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardFBS3()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "FBSNext3")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardFBS4()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "FBSNext4")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardFBS5()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "FBSNext5")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardFBSComplete()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Завершить", "FBSComplete")
            },
        });
        }
        /// ///////////////////////////////////////////////////////////
        public static InlineKeyboardMarkup GetNextKeyboard2FBS1()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "2FBSNext1")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard2FBS2()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "2FBSNext2")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard2FBS3()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "2FBSNext3")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard2FBS4()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "2FBSNext4")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard2FBS5()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "2FBSNext5")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard2FBSComplete()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Завершить", "2FBSComplete")
            },
        });
        }
        /// ///////////////////////////////////////////////////////////
        public static InlineKeyboardMarkup GetNextKeyboard3FBS1()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "3FBSNext1")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard3FBS2()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "3FBSNext2")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard3FBS3()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "3FBSNext3")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard3FBS4()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "3FBSNext4")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard3FBS5()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "3FBSNext5")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboard3FBSComplete()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Завершить", "3FBSComplete")
            },
        });
        }
        /// ///////////////////////////////////////////////////////////
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay1_1()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay1_1")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay1_2()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay1_2")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay1_3()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay1_3")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay1_4()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay1_4")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay1_5()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay1_5")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay1_Complete()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Завершить", "SplitDay1_Complete")
            },
        });
        }
        ///////////////////////////////////////////////////////////
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay2_1()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay2_1")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay2_2()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay2_2")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay2_3()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay2_3")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay2_4()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay2_4")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay2_5()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay2_5")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay2_Complete()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Завершить", "SplitDay2_Complete")
            },
        });
        }
        ///////////////////////////////////////////////////////////
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay3_1()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay3_1")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay3_2()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay3_2")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay3_3()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay3_3")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay3_4()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay3_4")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay3_5()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Готово", "SplitDay3_5")
            },
        });
        }
        public static InlineKeyboardMarkup GetNextKeyboardSplitDay3_Complete()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithCallbackData("Завершить", "SplitDay3_Complete")
            },
        });
        }

        public static InlineKeyboardMarkup GetArticlesKeyboard()
        {
            return new(new[]
            {
            new[]
            {
            InlineKeyboardButton.WithUrl("Як обрати тренера", "https://sportplaza.com.ua/2020/08/27/%d1%8f%d0%ba-%d0%be%d0%b1%d1%80%d0%b0%d1%82%d0%b8-%d1%82%d1%80%d0%b5%d0%bd%d0%b5%d1%80%d0%b0/"),
            InlineKeyboardButton.WithUrl("Скільки пити води під час тренування?", "https://sportplaza.com.ua/2020/08/10/%d1%81%d0%ba%d1%96%d0%bb%d1%8c%d0%ba%d0%b8-%d1%96-%d1%8f%d0%ba-%d0%bf%d0%be%d1%82%d1%80%d1%96%d0%b1%d0%bd%d0%be-%d0%bf%d0%b8%d1%82%d0%b8-%d0%bf%d1%96%d0%b4-%d1%87%d0%b0%d1%81-%d1%82%d1%80%d0%b5%d0%bd/"),
            },
            new[]
            {
            InlineKeyboardButton.WithUrl("Мануал по тренажерах", "https://sportplaza.com.ua/2020/09/19/%d1%87%d0%b0%d1%81%d1%82%d1%96-%d0%bf%d0%be%d0%bc%d0%b8%d0%bb%d0%ba%d0%b8-%d0%b2-%d0%b7%d0%b0%d0%bb%d1%96-%d1%8f%d0%ba-%d0%bd%d0%b5-%d1%82%d1%80%d0%b5%d0%b1%d0%b0-%d1%80%d0%be%d0%b1%d0%b8%d1%82%d0%b8/"),
            InlineKeyboardButton.WithUrl("З чого почати в залі", "https://sportplaza.com.ua/2020/10/15/%d0%b7-%d1%87%d0%be%d0%b3%d0%be-%d0%bf%d0%be%d1%87%d0%b0%d1%82%d0%b8-%d0%b2-%d0%b7%d0%b0%d0%bb%d1%96/")
            },
            new[]
            {
            InlineKeyboardButton.WithUrl("Скільки відпочивати між тренуваннями?", "https://sportplaza.com.ua/2020/11/04/%d1%81%d0%ba%d1%96%d0%bb%d1%8c%d0%ba%d0%b8-%d0%b2%d1%96%d0%b4%d0%bf%d0%be%d1%87%d0%b8%d0%b2%d0%b0%d1%82%d0%b8-%d0%bc%d1%96%d0%b6-%d1%82%d1%80%d0%b5%d0%bd%d1%83%d0%b2%d0%b0%d0%bd%d0%bd%d1%8f%d0%bc/"),
            InlineKeyboardButton.WithUrl("Поширені помилки в зале❌", "https://sportplaza.com.ua/2020/09/26/gym-mistakes/")
            },
            new[]
            {
            InlineKeyboardButton.WithUrl("Сила волі у спорті", "https://sportplaza.com.ua/2020/09/17/%d1%81%d0%b8%d0%bb%d0%b0-%d0%b2%d0%be%d0%bb%d1%96-%d1%83-%d1%81%d0%bf%d0%be%d1%80%d1%82%d1%96/"),
            InlineKeyboardButton.WithUrl("Чи необхідне кардіо для схуднення?", "https://sportplaza.com.ua/2020/11/26/%d1%87%d0%b8-%d0%bd%d0%b5%d0%be%d0%b1%d1%85%d1%96%d0%b4%d0%bd%d0%b5-%d0%ba%d0%b0%d1%80%d0%b4%d1%96%d0%be-%d0%b4%d0%bb%d1%8f-%d1%81%d1%85%d1%83%d0%b4%d0%bd%d0%b5%d0%bd%d0%bd%d1%8f/")
            }
        });
        }
    }
}
