using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
    //5種大型特殊建築物（占2個建築物空間）各一
　　//12種小型特殊建物（占1個建築物空間）各二
　　//20個生產用途建築物
{
    //
    //生廠廠房
    //
    internal class IndigoPlant : Building
    {
        public IndigoPlant(int type)
        {
            if (type == 0)
            {
                Level = 1;
                Name = "IndigoPlant_Small";
                Industry = "Indigo";
                worker = 0;
                cost = 1;
                MaxWorker = 1;
                Score = 1;
            }else if (type == 1)
            {
                Level = 2;
                Name = "IndigoPlant_Big";
                Industry = "Indigo";
                worker = 0;
                cost = 3;
                MaxWorker = 3;
                Score = 2;
            }
        }
    }
    internal class SugarMill : Building
    {
        public SugarMill(int type)
        {
            if (type == 0)
            {
                Level = 1;
                Name = "SugarMill_Small";
                Industry = "Sugar";
                worker = 0;
                cost = 2;
                MaxWorker = 1;
                Score = 1;

            }
            else if (type == 1)
            {
                Level = 2;
                Name = "SugarMill_Big";
                Industry = "Sugar";
                worker = 0;
                cost = 4;
                MaxWorker = 3;
                Score = 2;
            }
        }
    }
    internal class TobaccoStorage : Building
    {
        public TobaccoStorage()
        {
            Level = 3;
            Name = "TobaccoStorage";
            Industry = "Tobacco";
            worker = 0;
            cost = 5;
            MaxWorker = 3;
            Score = 3;
        }
    }
    internal class CoffeeRoaster : Building
    {
        public CoffeeRoaster()
        {
            Level = 3;
            Name = "CoffeeRoaster";
            Industry = "Coffee";
            worker = 0;
            cost = 6;
            MaxWorker = 2;
            Score = 3;
        }
    }
    //
    //小型特殊功能建築
    //
    internal class Smallmarket : Building
    {
        public Smallmarket(int type)//小市場
        {
            Level = 1;
            Name = "Smallmarket";
            Industry = "";
            worker = 0;
            cost = 1;
            MaxWorker = 1;
            //Score = 0;
        }
    }
    internal class Largemarket : Building
    {
        public Largemarket(int type)//大市場
        {
            Level = 2;
            Name = "Largemarket";
            Industry = "";
            worker = 0;
            cost = 5;
            MaxWorker = 1;
            //Score = 0;
        }
    }

    internal class Hacienda : Building
    {
        public Hacienda(int type)//農莊
        {
            Level = 1;
            Name = "Hacienda";
            Industry = "";
            worker = 0;
            cost = 2;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Constructionhut : Building
    {
        public Constructionhut(int type)//建築舍
        {
            Level = 1;
            Name = "Constructionhut";
            Industry = "";
            worker = 0;
            cost = 2;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Smallwarehouse : Building
    {
        public Smallwarehouse(int type)//小倉庫
        {
            Level = 1;
            Name = "Smallwarehouse";
            Industry = "";
            worker = 0;
            cost = 3;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Largewarehouse : Building
    {
        public Largewarehouse(int type)//大倉庫
        {
            Level = 2;
            Name = "Largewarehouse";
            Industry = "";
            worker = 0;
            cost = 6;
            MaxWorker = 0;
            //Score = 0;
        }
    }

    internal class Hospice : Building
    {
        public Hospice(int type)//民宿
        {
            Level = 2;
            Name = "Hospice";
            Industry = "";
            worker = 0;
            cost = 4;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Office : Building
    {
        public Office(int type)//辦公室
        {
            Level = 2;
            Name = "Office";
            Industry = "";
            worker = 0;
            cost = 5;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Factory : Building
    {
        public Factory(int type)//大工廠
        {
            Level = 3;
            Name = "Factory";
            Industry = "";
            worker = 0;
            cost = 7;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class University : Building
    {
        public University(int type)//大學
        {
            Level = 3;
            Name = "University";
            Industry = "";
            worker = 0;
            cost = 8;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Harbor : Building
    {
        public Harbor(int type)//港口
        {
            Level = 3;
            Name = "Harbor";
            Industry = "";
            worker = 0;
            cost = 8;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Wharf : Building
    {
        public Wharf(int type)//碼頭
        {
            Level = 3;
            Name = "Wharf";
            Industry = "";
            worker = 0;
            cost = 9;
            MaxWorker = 0;
            //Score = 0;
        }
    }

    //
    //大型特殊功能建築
    //
    internal class Guildhall : Building
    {
        public Guildhall(int type)//商會
        {
            Level = 4;
            Name = "Guildhall";
            Industry = "";
            worker = 0;
            cost = 10;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Residence : Building
    {
        public Residence(int type)//居民區
        {
            Level = 4;
            Name = "Residence";
            Industry = "";
            worker = 0;
            cost = 10;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Fortress : Building
    {
        public Fortress(int type)//要塞
        {
            Level = 4;
            Name = "Fortress";
            Industry = "";
            worker = 0;
            cost = 10;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Customshouse : Building
    {
        public Customshouse(int type)//海關
        {
            Level = 4;
            Name = "Customshouse";
            Industry = "";
            worker = 0;
            cost = 10;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    internal class Cityhall : Building
    {
        public Cityhall(int type)//市政廳
        {
            Level = 4;
            Name = "Cityhall";
            Industry = "";
            worker = 0;
            cost = 10;
            MaxWorker = 0;
            //Score = 0;
        }
    }
    //
    //空建築，當作PASS
    //
    internal class PassBuilding : Building
    {
        public PassBuilding()
        {
            Name = "PassBuilding";
        }
    }
}
