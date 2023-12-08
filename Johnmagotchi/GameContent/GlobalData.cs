using Johnmagotchi.GameContent.Objects.Johns;
using Johnmagotchi.GameContent.Objects.Johns.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibzGame.Core.ScreenManager;

namespace Johnmagotchi.GameContent
{
    public class GlobalData
    {
        public double johnPoints;
        public enum JohnList
        {
            BaseJohn,
            AccountantJohn,
            GrizzlyJohn,
            NerdJohn,
            SportsJohn,
            Johntrepreneur,
            JohnUgly,
            JohnCena,
            PapaJohn
        }

        public bool isBaseJohnPurchased = true;
        public bool isAccountingJohnPurchased = false;
        public bool isGrizzlyJohnPurchased = false;
        public bool isNerdJohnPurchased = false;
        public bool isSportsJohnPurchased = false;
        public bool isJohntrePurchased = false;
        public bool isJohnUglyPurchased = false;
        public bool isJohnCenaPurchased = false;
        public bool isPapaJohnPurchased = false;
        ScreenManager screenManager;



        public GlobalData(ScreenManager sm) {
        screenManager = sm;
        johnPoints = 0; 
        }   
        
        public  IAbstractJohn GetJohnByEnum( JohnList johnEnum)
        {
            IAbstractJohn returnJohn;

            switch (johnEnum)
            {
            case JohnList.BaseJohn:

                returnJohn = new BaseJohn();
                break;

            case JohnList.AccountantJohn:
                returnJohn = new AccountantJohn();
                break;
            case JohnList.GrizzlyJohn:
                returnJohn = new GrizzlyJohn();
                break;
            case JohnList.NerdJohn:
                returnJohn = new NerdJohn();
                break;
            case JohnList.SportsJohn:
                returnJohn = new SportsJohn();
                break;
            case JohnList.Johntrepreneur:
                returnJohn = new EntreJohn();
                break;
            case JohnList.JohnUgly:
                returnJohn = new UglyJohn();
                break;
            case JohnList.JohnCena:
                returnJohn = new CenaJohn();
                break;
            case JohnList.PapaJohn:
                returnJohn = new PapaJohn();
                break;

                default:
                    return null;
                  
            }
            returnJohn.Init(screenManager);
            return returnJohn;
        }

        public bool IsPurchased(IAbstractJohn john)
        {
           JohnList johnEnum =  john.getJohnEnum();
            switch (johnEnum)
            {
                case JohnList.BaseJohn:
                    return isBaseJohnPurchased;

                case JohnList.AccountantJohn:
                    return isAccountingJohnPurchased;

                case JohnList.GrizzlyJohn:
                    return isGrizzlyJohnPurchased;

                case JohnList.NerdJohn:
                    return isNerdJohnPurchased;

                case JohnList.SportsJohn:
                    return isSportsJohnPurchased;

                case JohnList.Johntrepreneur:
                    return isJohntrePurchased;

                case JohnList.JohnUgly:
                    return isJohnUglyPurchased;

                case JohnList.JohnCena:
                    return isJohnCenaPurchased;

                case JohnList.PapaJohn:
                    return isPapaJohnPurchased;

            }

            return false;
        }

        public void PurchaseJohn(IAbstractJohn john)
        {
            Debug.WriteLine(john.GetDisplayName());
            JohnList johnEnum = john.getJohnEnum();
            johnPoints -= john.getShopCost();
            switch (johnEnum)
            {
                case JohnList.BaseJohn:
                    isBaseJohnPurchased = true;
                    Debug.WriteLine("basePurchase");
                    break; 

                case JohnList.AccountantJohn:
                    isAccountingJohnPurchased = true;
                    Debug.WriteLine("acctPurchase");
                    break;

                case JohnList.GrizzlyJohn:
                     isGrizzlyJohnPurchased = true;
                    Debug.WriteLine("grizzPurchase");
                    break;

                case JohnList.NerdJohn:
                     isNerdJohnPurchased = true;
                    break;

                case JohnList.SportsJohn:
                     isSportsJohnPurchased = true;
                    break;

                case JohnList.Johntrepreneur:
                     isJohntrePurchased = true;
                    break;

                case JohnList.JohnUgly:
                     isJohnUglyPurchased = true;
                    break;

                case JohnList.JohnCena:
                     isJohnCenaPurchased = true;
                    break;

                case JohnList.PapaJohn:
                     isPapaJohnPurchased = true;
                    break;

            }  
        }
    }
}
