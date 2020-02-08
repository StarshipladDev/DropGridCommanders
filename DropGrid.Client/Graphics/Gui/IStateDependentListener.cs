namespace DropGrid.Client.Graphics.Gui
{
    /// <summary>
    /// A listener to subscribe to the <c>Enabled</c> status update of another <c>AbstractComponent</c>.<br />
    /// A component X is state-dependent on another component, Y, if X is enabled iff Y is enabled.  
    /// </summary>
    public interface IStateDependentListener
    {
        void StateChanged(bool dependentEnabled);
    }
}