public class CloseWindowsButton : UIButton
{
    protected override void OnButtonClick()
    {
        WindowsCloser.CloseAllAndContinueGame();
    }
}