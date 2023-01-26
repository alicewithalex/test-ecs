using System.Text;
using UnityEngine;

namespace alicewithalex.FancyDebug
{
    public static class FDebug
    {
        public const char SPLITTER = '#';

        private static StringBuilder _stringBuilder = new StringBuilder();

        public static void Log(string message, params FColor[] colors)
        {
            if (colors == null || colors.Length == 0)
            {
                Debug.Log(message);
                return;
            }

            int i = 0;

            bool coloring = false;
            foreach (var ch in message)
            {
                if (ch == SPLITTER)
                {
                    if (coloring)
                    {
                        coloring = false;
                        i++;
                        _stringBuilder.Append("</color>");
                    }
                    else
                    {
                        coloring = true;

                        _stringBuilder.Append($"<color=#" +
                            $"{colors[i % colors.Length].ToHex()}>");
                    }

                    continue;
                }

                _stringBuilder.Append(ch);
            }

            Debug.Log(_stringBuilder.ToString());

            _stringBuilder.Clear();
        }

        public static void Log(string message, char enclosure = '#',
            params FColor[] colors)
        {
            if (colors == null || colors.Length == 0)
            {
                Debug.Log(message);
                return;
            }

            int i = 0;

            bool coloring = false;
            foreach (var ch in message)
            {
                if (ch == enclosure)
                {
                    if (coloring)
                    {
                        coloring = false;
                        i++;
                        _stringBuilder.Append("</color>");
                    }
                    else
                    {
                        coloring = true;

                        _stringBuilder.Append($"<color=#" +
                            $"{colors[i % colors.Length].ToHex()}>");
                    }

                    continue;
                }

                _stringBuilder.Append(ch);
            }

            Debug.Log(_stringBuilder.ToString());

            _stringBuilder.Clear();
        }
    }
}