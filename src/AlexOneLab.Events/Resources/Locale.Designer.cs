﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlexOneLab.Events.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Locale {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Locale() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AlexOneLab.Events.Resources.Locale", typeof(Locale).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No countdown for this event was found.
        /// </summary>
        internal static string CountdownNotFound {
            get {
                return ResourceManager.GetString("CountdownNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Countdown stopped.
        /// </summary>
        internal static string CountdownStopped {
            get {
                return ResourceManager.GetString("CountdownStopped", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event not found.
        /// </summary>
        internal static string EventNotFound {
            get {
                return ResourceManager.GetString("EventNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Event removed from schedule.
        /// </summary>
        internal static string EventRemoved {
            get {
                return ResourceManager.GetString("EventRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New event scheduled.
        /// </summary>
        internal static string EventScheduled {
            get {
                return ResourceManager.GetString("EventScheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Start.
        /// </summary>
        internal static string EventStart {
            get {
                return ResourceManager.GetString("EventStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid date format.
        /// </summary>
        internal static string InvalidDateFormat {
            get {
                return ResourceManager.GetString("InvalidDateFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid time format.
        /// </summary>
        internal static string InvalidTimeFormat {
            get {
                return ResourceManager.GetString("InvalidTimeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No upcoming events.
        /// </summary>
        internal static string NoUpcomingEvents {
            get {
                return ResourceManager.GetString("NoUpcomingEvents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Until {0} remain{1:cond:&gt;0? {1} {1:day|days}|}{2:cond:&gt;0? {2} {2:hour|hours}|}{3:cond:&gt;0? {3} {3:minute|minutes}|}.
        /// </summary>
        internal static string UntilEvent {
            get {
                return ResourceManager.GetString("UntilEvent", resourceCulture);
            }
        }
    }
}