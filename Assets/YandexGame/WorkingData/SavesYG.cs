
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";
        public bool feedbackDone;
        public bool promptDone;
        public int languageIndex = 0;

        // Ваши сохранения
        public int money = 300;
        public int score = 0;
    }
}