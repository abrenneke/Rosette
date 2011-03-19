using System;
using System.Collections.Generic;
using Rosette.Data;
using Rosette.Fractals;

namespace Rosette.Windows
{
    /// <summary>Provides history to a fractal zooming, allowing going forward and back.</summary>
    public class FractalHistoryProvider
    {
        private Stack<RenderedColoredFractal> historyStack = new Stack<RenderedColoredFractal>();

        /// <summary>Delegate to be called when Back is called.</summary>
        public event Action<RenderedColoredFractal> OnBack;

        /// <summary>Moves the history one point back.</summary>
        public void Back()
        {
            if (historyStack.Count < 2)
                return;

            historyStack.Pop();
            var newRendered = historyStack.Peek();

            if(OnBack != null)
                OnBack(newRendered);
        }
        
        /// <summary>Pushes a new bounds on to the history.</summary>
        public void Push(RenderedColoredFractal renderedFractal)
        {
            historyStack.Push(renderedFractal);
        }
    }
}
