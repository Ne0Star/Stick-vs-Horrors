
using UnityEngine;

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
        /// <summary>
        /// Предметы игрока
        /// </summary>
        public InventoryData[] items = null;
        /// <summary>
        /// Монеты
        /// </summary>
        public int Money = 300;
        /// <summary>
        /// Рекорд
        /// </summary>
        public int Score = 0;
    }

    [System.Serializable]
    public struct InventoryData
    {
        public int id;
        public int count;
    }
}