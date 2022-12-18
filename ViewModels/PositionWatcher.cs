using Flowchart_Framework.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Flowchart_Framework.ViewModels
{
    public class PositionChangeEventArgs : EventArgs
    {
        public readonly Point Point;
        public PositionChangeEventArgs(Point Point) => Point = Point;
    }
    
    public class PositionWatcher
    {

        private FrameworkElement _target, _origin;
        private Point _currentRendererPoint;
        public event EventHandler<PositionChangeEventArgs> Changed;

        public void ChangeTarget(FrameworkElement target, FrameworkElement origin = null)
        {
            if (_target != null)
            {
                _target.LayoutUpdated -= OnPositionUpdate;
            }

            _target = target;
            _origin = origin;
            OnPositionUpdate(null, null);

            if (_target != null)
            {
                _target.LayoutUpdated += OnPositionUpdate;
            }
        }

        private void OnPositionUpdate(object sender, EventArgs e)
        {
            Point newRendererPoint = GetRendererPoint();
            if (newRendererPoint != _currentRendererPoint)
            {
                _currentRendererPoint = newRendererPoint;
                Changed?.Invoke(_target, new PositionChangeEventArgs(_currentRendererPoint));
            }
        }

        public static Point ComputeRendererPoint(FrameworkElement target, FrameworkElement origin)
        {
            return target.TranslatePoint(new Point(), origin);
        }

        private Point GetRendererPoint()
        {
            return ComputeRendererPoint(_target, _origin);
        }

    }
}
