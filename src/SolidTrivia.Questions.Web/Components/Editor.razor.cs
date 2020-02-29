using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace SolidTrivia.Questions.Web.Shared
{
    public class EditorBase : ComponentBase
    {
        [Parameter] public string InitialValue { get; set; }

        [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }

        protected async Task InputChanged(ChangeEventArgs args)
        {
            await OnChange.InvokeAsync(args);
        }
    }
}