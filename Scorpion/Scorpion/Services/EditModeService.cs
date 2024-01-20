namespace Scorpion.Services
{
    public class EditModeService
    {
        public delegate void EditModeChangedHandler(bool value);

        private static bool _isEditMode;

        public static bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                EditModeChanged?.Invoke(value);
            }
        }
        public static event EditModeChangedHandler EditModeChanged;

    }
}