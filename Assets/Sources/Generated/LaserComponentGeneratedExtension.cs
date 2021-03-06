//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGenerator.ComponentExtensionsGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Entitas {
    public partial class Entity {
        static readonly LaserComponent laserComponent = new LaserComponent();

        public bool isLaser {
            get { return HasComponent(ComponentIds.Laser); }
            set {
                if (value != isLaser) {
                    if (value) {
                        AddComponent(ComponentIds.Laser, laserComponent);
                    } else {
                        RemoveComponent(ComponentIds.Laser);
                    }
                }
            }
        }

        public Entity IsLaser(bool value) {
            isLaser = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherLaser;

        public static IMatcher Laser {
            get {
                if (_matcherLaser == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Laser);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherLaser = matcher;
                }

                return _matcherLaser;
            }
        }
    }
}
