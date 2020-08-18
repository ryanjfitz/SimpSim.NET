using Prism.Services.Dialogs;
using SimpSim.NET.WPF.Views;

namespace SimpSim.NET.WPF
{
    public interface IDialogServiceAdapter
    {
        void ShowAssemblyEditorDialog(string text = null);
    }

    internal class DialogServiceAdapter : IDialogServiceAdapter
    {
        private readonly IDialogService _dialogService;

        public DialogServiceAdapter(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public void ShowAssemblyEditorDialog(string text = null)
        {
            _dialogService.ShowDialog(nameof(AssemblyEditorDialog), new DialogParameters { { "text", text } }, result => { });
        }
    }
}