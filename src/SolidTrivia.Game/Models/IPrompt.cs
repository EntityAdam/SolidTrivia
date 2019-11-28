using System;

namespace SolidTrivia.Game.Models
{
    public interface IPrompt
    {
        string[] AcceptableResponses { get; set; }
        string PromptText { get; set; }
        string Code { get; set; }
        bool HasCode { get; }
        Guid Id { get; set; }
        bool IsAnswered { get; set; }
        bool IsAnswering { get; set; }
        int Weight { get; set; }
        void MarkAsAnswered();
        void MarkAsCurrentPrompt();
    }
}