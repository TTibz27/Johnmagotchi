using Johnmagotchi.Core.tools;
using Johnmagotchi.GameContent.Screens.BattleMapScreens.BattleMapScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.GameContent.Screens.Menu.BattleMap
{
    internal class AfterMovementMenu :BaseMapMenu
    {
        private BattleMapScreen BattleMapInstance;
        public AfterMovementMenu(BattleMapScreen parentScreen, int x, int y, int screenQuadrant, bool isAttackAvailable) : base(parentScreen, x, y, screenQuadrant)
        {
            this.isUpdatePriority = true;
            this.isDrawPriority = false;
            BattleMapInstance = parentScreen;
            if (isAttackAvailable)
            {
                CurrentOptions = new MenuOption[3];
                CurrentOptions[0] = new MenuOption(MenuOption.MenuOptionType.ATTACK);
                CurrentOptions[1] = new MenuOption(MenuOption.MenuOptionType.ITEM);
                CurrentOptions[2] = new MenuOption(MenuOption.MenuOptionType.WAIT);
            }
            else {
                CurrentOptions = new MenuOption[2];
                CurrentOptions[0] = new MenuOption(MenuOption.MenuOptionType.ITEM);
                CurrentOptions[1] = new MenuOption(MenuOption.MenuOptionType.WAIT);
            }
          
            if (screenQuadrant == 1)
            {
                xDrawOffset = 7000;
                yDrawOffset = -2800 * (CurrentOptions.Length - 1) - 300;
            }
            else if (screenQuadrant == 2)
            {
                xDrawOffset = -3800;
                yDrawOffset = -2800 * (CurrentOptions.Length - 1) - 300;
            }
            else if (screenQuadrant == 3)
            {
                xDrawOffset = -3800;
                yDrawOffset = 0;
            }
            else
            {
                xDrawOffset = 7000;
                yDrawOffset = 0;
            }
        }
        public override void ChildInit()
        {
            //  throw new NotImplementedException();
        }

        public override void ChildUpdate()
        {
            if (screenManager.inputs.editorInputs.confirm.isJustPressed)
            {
                int subX = xPos;
                int subY = yPos;
                if (screenQuadrant == 1)
                {
                    subX -= 800;
                    subY += 800;
                }
                else if (screenQuadrant == 2)
                {
                    subX += 800;
                    subY += 800;
                }
                else if (screenQuadrant == 3)
                {
                    subX += 800;
                    subY -= ((CurrentOptions.Length - 3) * 2800) - 800;
                }
                else
                {
                    subX -= 800;
                    subY -= ((CurrentOptions.Length - 3) * 2800) - 800;

                }

                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.ATTACK)
                {
                    screenManager.removeTopScreens(1);
                    BattleMapInstance.AfterMoveAttack();
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.ITEM)
                {
                    screenManager.removeTopScreens(1);
                    BattleMapInstance.AfterMoveItem();
                }
                if (CurrentOptions[SelectedIndex].OptionType == MenuOption.MenuOptionType.WAIT)
                {             
                    screenManager.removeTopScreens(1);  
                    BattleMapInstance.AfterMoveWait();
                }
            }
            if (screenManager.inputs.editorInputs.cancel.isJustPressed) {
                screenManager.removeTopScreens(1);
                BattleMapInstance.AfterMoveCancelled();
            }

        }
    }
}
