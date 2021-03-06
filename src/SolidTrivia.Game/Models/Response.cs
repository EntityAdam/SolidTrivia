﻿using System;

namespace SolidTrivia.Game.Models
{
    public class Response
    {
        public Response(string playerId, Guid answerId, int weight, string text, bool isCorrect, DateTime time)
        {
            PlayerId = playerId;
            AnswerId = answerId;
            Weight = weight;
            Text = text;
            IsCorrect = isCorrect;
            Time = time;
        }

        public string PlayerId { get; }

        public Guid AnswerId { get; }

        public string Text { get; }

        public bool IsCorrect { get; }

        public DateTime Time { get; }

        private int Weight { get; }

        public int Score => IsCorrect ? (Weight * 100) : (Weight * -1 * 100);
    }
}