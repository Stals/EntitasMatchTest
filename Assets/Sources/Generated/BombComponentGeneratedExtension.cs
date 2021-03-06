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
        static readonly BombComponent bombComponent = new BombComponent();

        public bool isBomb {
            get { return HasComponent(ComponentIds.Bomb); }
            set {
                if (value != isBomb) {
                    if (value) {
                        AddComponent(ComponentIds.Bomb, bombComponent);
                    } else {
                        RemoveComponent(ComponentIds.Bomb);
                    }
                }
            }
        }

        public Entity IsBomb(bool value) {
            isBomb = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherBomb;

        public static IMatcher Bomb {
            get {
                if (_matcherBomb == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Bomb);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherBomb = matcher;
                }

                return _matcherBomb;
            }
        }
    }
}
