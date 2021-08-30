using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Garten
{
    public class HideTabLabelsEffect : RoutingEffect
    {
        public HideTabLabelsEffect()
            : base($"AppEffects.{nameof(HideTabLabelsEffect)}")
        {
        }
    }
}
