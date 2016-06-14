﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vulcan.Wpf.Core
{
    public class ViewModelBase : MvvmObject
    {
        // This will trigger when a Vulcan.Wpf.Core.View is Loaded and has this object as its DataContext
        // TODO: This is probably a bad idea since a viewModel can be the data context of more than one view
        public virtual void OnViewLoaded()
        {
        }

        #region VisualState trigger

        // The following trigger has to be set on the Grid containing the VisualStateGroup:
        //<i:Interaction.Triggers>
        //    <ei:PropertyChangedTrigger Binding = "{Binding VisualStateChangeTrigger}">
        //        <ei:GoToStateAction StateName = "{Binding TargetVisualStateName}"/>
        //     </ei:PropertyChangedTrigger>
        //</i:Interaction.Triggers>

        public string TargetVisualStateName
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

        public bool VisualStateChangeTrigger
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        protected void triggerVisualState(string stateName)
        {
            TargetVisualStateName = stateName;
            VisualStateChangeTrigger = !VisualStateChangeTrigger;
        }

        #endregion VisualState trigger
    }
}
