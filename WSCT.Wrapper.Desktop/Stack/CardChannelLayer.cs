using WSCT.Core;
using WSCT.Stack;
using WSCT.Wrapper.Desktop.Core;

namespace WSCT.Wrapper.Desktop.Stack
{
    /// <summary>
    /// Implements <see cref="CardChannel"/> as a <see cref="CardChannelLayer"/>.
    /// </summary>
    /// <remarks>
    /// This layer is the terminal (top) layer by design.
    /// </remarks>
    public class CardChannelLayer : CardChannel, ICardChannelLayer
    {
        #region >> Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public CardChannelLayer()
        {
        }

        /// <inheritdoc cref="CardChannel(WSCT.Core.ICardContext,string)"/>
        public CardChannelLayer(ICardContext context, string readerName)
            : base(context, readerName)
        {
        }

        #endregion

        #region >> ICardChannelLayer Membres

        /// <inheritdoc />
        public void SetStack(ICardChannelStack stack)
        {
            // Nothing to do here.
        }

        /// <inheritdoc />
        public string LayerId
        {
            get { return "PC/SC"; }
        }

        #endregion
    }
}