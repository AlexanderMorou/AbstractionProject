using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Properties;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public static class CSCompilerMessages
    {
        public static ICompilerReferenceWarning CS0028
        {
            get
            {
                if (_CS0028 == null)
                    _CS0028 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0028, 4, 28);
                return _CS0028;
            }
        }
        private static ICompilerReferenceWarning _CS0028;
        public static ICompilerReferenceWarning CS0067
        {
            get
            {
                if (_CS0067 == null)
                    _CS0067 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0067, 3, 67);
                return _CS0067;
            }
        }
        private static ICompilerReferenceWarning _CS0067;
        public static ICompilerReferenceWarning CS0078
        {
            get
            {
                if (_CS0078 == null)
                    _CS0078 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0078, 4, 78);
                return _CS0078;
            }
        }
        private static ICompilerReferenceWarning _CS0078;
        public static ICompilerReferenceWarning CS0105
        {
            get
            {
                if (_CS0105 == null)
                    _CS0105 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0105, 3, 105);
                return _CS0105;
            }
        }
        private static ICompilerReferenceWarning _CS0105;
        public static ICompilerReferenceWarning CS0108
        {
            get
            {
                if (_CS0108 == null)
                    _CS0108 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0108, 2, 108);
                return _CS0108;
            }
        }
        private static ICompilerReferenceWarning _CS0108;
        public static ICompilerReferenceWarning CS0109
        {
            get
            {
                if (_CS0109 == null)
                    _CS0109 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0109, 4, 109);
                return _CS0109;
            }
        }
        private static ICompilerReferenceWarning _CS0109;
        public static ICompilerReferenceWarning CS0114
        {
            get
            {
                if (_CS0114 == null)
                    _CS0114 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0114, 2, 114);
                return _CS0114;
            }
        }
        private static ICompilerReferenceWarning _CS0114;
        public static ICompilerReferenceWarning CS0162
        {
            get
            {
                if (_CS0162 == null)
                    _CS0162 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0162, 2, 162);
                return _CS0162;
            }
        }
        private static ICompilerReferenceWarning _CS0162;
        public static ICompilerReferenceWarning CS0164
        {
            get
            {
                if (_CS0164 == null)
                    _CS0164 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0164, 2, 164);
                return _CS0164;
            }
        }
        private static ICompilerReferenceWarning _CS0164;
        public static ICompilerReferenceWarning CS0168
        {
            get
            {
                if (_CS0168 == null)
                    _CS0168 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0168, 3, 168);
                return _CS0168;
            }
        }
        private static ICompilerReferenceWarning _CS0168;
        public static ICompilerReferenceWarning CS0169
        {
            get
            {
                if (_CS0169 == null)
                    _CS0169 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0169, 3, 169);
                return _CS0169;
            }
        }
        private static ICompilerReferenceWarning _CS0169;
        public static ICompilerReferenceWarning CS0183
        {
            get
            {
                if (_CS0183 == null)
                    _CS0183 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0183, 1, 183);
                return _CS0183;
            }
        }
        private static ICompilerReferenceWarning _CS0183;
        public static ICompilerReferenceWarning CS0184
        {
            get
            {
                if (_CS0184 == null)
                    _CS0184 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0184, 1, 184);
                return _CS0184;
            }
        }
        private static ICompilerReferenceWarning _CS0184;
        public static ICompilerReferenceWarning CS0197
        {
            get
            {
                if (_CS0197 == null)
                    _CS0197 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0197, 1, 197);
                return _CS0197;
            }
        }
        private static ICompilerReferenceWarning _CS0197;
        public static ICompilerReferenceWarning CS0219
        {
            get
            {
                if (_CS0219 == null)
                    _CS0219 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0219, 3, 219);
                return _CS0219;
            }
        }
        private static ICompilerReferenceWarning _CS0219;
        public static ICompilerReferenceWarning CS0251
        {
            get
            {
                if (_CS0251 == null)
                    _CS0251 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0251, 2, 251);
                return _CS0251;
            }
        }
        private static ICompilerReferenceWarning _CS0251;
        public static ICompilerReferenceWarning CS0252
        {
            get
            {
                if (_CS0252 == null)
                    _CS0252 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0252, 2, 252);
                return _CS0252;
            }
        }
        private static ICompilerReferenceWarning _CS0252;
        public static ICompilerReferenceWarning CS0253
        {
            get
            {
                if (_CS0253 == null)
                    _CS0253 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0253, 2, 253);
                return _CS0253;
            }
        }
        private static ICompilerReferenceWarning _CS0253;
        public static ICompilerReferenceWarning CS0278
        {
            get
            {
                if (_CS0278 == null)
                    _CS0278 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0278, 2, 278);
                return _CS0278;
            }
        }
        private static ICompilerReferenceWarning _CS0278;
        public static ICompilerReferenceWarning CS0279
        {
            get
            {
                if (_CS0279 == null)
                    _CS0279 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0279, 2, 279);
                return _CS0279;
            }
        }
        private static ICompilerReferenceWarning _CS0279;
        public static ICompilerReferenceWarning CS0280
        {
            get
            {
                if (_CS0280 == null)
                    _CS0280 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0280, 2, 280);
                return _CS0280;
            }
        }
        private static ICompilerReferenceWarning _CS0280;
        public static ICompilerReferenceWarning CS0282
        {
            get
            {
                if (_CS0282 == null)
                    _CS0282 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0282, 3, 282);
                return _CS0282;
            }
        }
        private static ICompilerReferenceWarning _CS0282;
        public static ICompilerReferenceWarning CS0402
        {
            get
            {
                if (_CS0402 == null)
                    _CS0402 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0402, 4, 402);
                return _CS0402;
            }
        }
        private static ICompilerReferenceWarning _CS0402;
        public static ICompilerReferenceWarning CS0414
        {
            get
            {
                if (_CS0414 == null)
                    _CS0414 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0414, 3, 414);
                return _CS0414;
            }
        }
        private static ICompilerReferenceWarning _CS0414;
        public static ICompilerReferenceWarning CS0419
        {
            get
            {
                if (_CS0419 == null)
                    _CS0419 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0419, 3, 419);
                return _CS0419;
            }
        }
        private static ICompilerReferenceWarning _CS0419;
        public static ICompilerReferenceWarning CS0420
        {
            get
            {
                if (_CS0420 == null)
                    _CS0420 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0420, 1, 420);
                return _CS0420;
            }
        }
        private static ICompilerReferenceWarning _CS0420;
        public static ICompilerReferenceWarning CS0422
        {
            get
            {
                if (_CS0422 == null)
                    _CS0422 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0422, 4, 422);
                return _CS0422;
            }
        }
        private static ICompilerReferenceWarning _CS0422;
        public static ICompilerReferenceWarning CS0429
        {
            get
            {
                if (_CS0429 == null)
                    _CS0429 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0429, 4, 429);
                return _CS0429;
            }
        }
        private static ICompilerReferenceWarning _CS0429;
        public static ICompilerReferenceWarning CS0435
        {
            get
            {
                if (_CS0435 == null)
                    _CS0435 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0435, 2, 435);
                return _CS0435;
            }
        }
        private static ICompilerReferenceWarning _CS0435;
        public static ICompilerReferenceWarning CS0436
        {
            get
            {
                if (_CS0436 == null)
                    _CS0436 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0436, 2, 436);
                return _CS0436;
            }
        }
        private static ICompilerReferenceWarning _CS0436;
        public static ICompilerReferenceWarning CS0437
        {
            get
            {
                if (_CS0437 == null)
                    _CS0437 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0437, 2, 437);
                return _CS0437;
            }
        }
        private static ICompilerReferenceWarning _CS0437;
        public static ICompilerReferenceWarning CS0440
        {
            get
            {
                if (_CS0440 == null)
                    _CS0440 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0440, 2, 440);
                return _CS0440;
            }
        }
        private static ICompilerReferenceWarning _CS0440;
        public static ICompilerReferenceWarning CS0444
        {
            get
            {
                if (_CS0444 == null)
                    _CS0444 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0444, 2, 444);
                return _CS0444;
            }
        }
        private static ICompilerReferenceWarning _CS0444;
        public static ICompilerReferenceWarning CS0458
        {
            get
            {
                if (_CS0458 == null)
                    _CS0458 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0458, 2, 458);
                return _CS0458;
            }
        }
        private static ICompilerReferenceWarning _CS0458;
        public static ICompilerReferenceWarning CS0464
        {
            get
            {
                if (_CS0464 == null)
                    _CS0464 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0464, 2, 464);
                return _CS0464;
            }
        }
        private static ICompilerReferenceWarning _CS0464;
        public static ICompilerReferenceWarning CS0465
        {
            get
            {
                if (_CS0465 == null)
                    _CS0465 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0465, 1, 465);
                return _CS0465;
            }
        }
        private static ICompilerReferenceWarning _CS0465;
        public static ICompilerReferenceWarning CS0467
        {
            get
            {
                if (_CS0467 == null)
                    _CS0467 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0467, 2, 467);
                return _CS0467;
            }
        }
        private static ICompilerReferenceWarning _CS0467;
        public static ICompilerReferenceWarning CS0469
        {
            get
            {
                if (_CS0469 == null)
                    _CS0469 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0469, 2, 469);
                return _CS0469;
            }
        }
        private static ICompilerReferenceWarning _CS0469;
        public static ICompilerReferenceWarning CS0472
        {
            get
            {
                if (_CS0472 == null)
                    _CS0472 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0472, 2, 472);
                return _CS0472;
            }
        }
        private static ICompilerReferenceWarning _CS0472;
        public static ICompilerReferenceWarning CS0602
        {
            get
            {
                if (_CS0602 == null)
                    _CS0602 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0602, 1, 602);
                return _CS0602;
            }
        }
        private static ICompilerReferenceWarning _CS0602;
        public static ICompilerReferenceWarning CS0612
        {
            get
            {
                if (_CS0612 == null)
                    _CS0612 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0612, 1, 612);
                return _CS0612;
            }
        }
        private static ICompilerReferenceWarning _CS0612;
        public static ICompilerReferenceWarning CS0618
        {
            get
            {
                if (_CS0618 == null)
                    _CS0618 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0618, 2, 618);
                return _CS0618;
            }
        }
        private static ICompilerReferenceWarning _CS0618;
        public static ICompilerReferenceWarning CS0626
        {
            get
            {
                if (_CS0626 == null)
                    _CS0626 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0626, 1, 626);
                return _CS0626;
            }
        }
        private static ICompilerReferenceWarning _CS0626;
        public static ICompilerReferenceWarning CS0628
        {
            get
            {
                if (_CS0628 == null)
                    _CS0628 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0628, 4, 628);
                return _CS0628;
            }
        }
        private static ICompilerReferenceWarning _CS0628;
        public static ICompilerReferenceWarning CS0642
        {
            get
            {
                if (_CS0642 == null)
                    _CS0642 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0642, 3, 642);
                return _CS0642;
            }
        }
        private static ICompilerReferenceWarning _CS0642;
        public static ICompilerReferenceWarning CS0649
        {
            get
            {
                if (_CS0649 == null)
                    _CS0649 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0649, 4, 649);
                return _CS0649;
            }
        }
        private static ICompilerReferenceWarning _CS0649;
        public static ICompilerReferenceWarning CS0652
        {
            get
            {
                if (_CS0652 == null)
                    _CS0652 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0652, 2, 652);
                return _CS0652;
            }
        }
        private static ICompilerReferenceWarning _CS0652;
        public static ICompilerReferenceWarning CS0657
        {
            get
            {
                if (_CS0657 == null)
                    _CS0657 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0657, 1, 657);
                return _CS0657;
            }
        }
        private static ICompilerReferenceWarning _CS0657;
        public static ICompilerReferenceWarning CS0658
        {
            get
            {
                if (_CS0658 == null)
                    _CS0658 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0658, 1, 658);
                return _CS0658;
            }
        }
        private static ICompilerReferenceWarning _CS0658;
        public static ICompilerReferenceWarning CS0659
        {
            get
            {
                if (_CS0659 == null)
                    _CS0659 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0659, 3, 659);
                return _CS0659;
            }
        }
        private static ICompilerReferenceWarning _CS0659;
        public static ICompilerReferenceWarning CS0660
        {
            get
            {
                if (_CS0660 == null)
                    _CS0660 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0660, 3, 660);
                return _CS0660;
            }
        }
        private static ICompilerReferenceWarning _CS0660;
        public static ICompilerReferenceWarning CS0661
        {
            get
            {
                if (_CS0661 == null)
                    _CS0661 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0661, 3, 661);
                return _CS0661;
            }
        }
        private static ICompilerReferenceWarning _CS0661;
        public static ICompilerReferenceWarning CS0665
        {
            get
            {
                if (_CS0665 == null)
                    _CS0665 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0665, 3, 665);
                return _CS0665;
            }
        }
        private static ICompilerReferenceWarning _CS0665;
        public static ICompilerReferenceWarning CS0672
        {
            get
            {
                if (_CS0672 == null)
                    _CS0672 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0672, 1, 672);
                return _CS0672;
            }
        }
        private static ICompilerReferenceWarning _CS0672;
        public static ICompilerReferenceWarning CS0675
        {
            get
            {
                if (_CS0675 == null)
                    _CS0675 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0675, 3, 675);
                return _CS0675;
            }
        }
        private static ICompilerReferenceWarning _CS0675;
        public static ICompilerReferenceWarning CS0693
        {
            get
            {
                if (_CS0693 == null)
                    _CS0693 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0693, 3, 693);
                return _CS0693;
            }
        }
        private static ICompilerReferenceWarning _CS0693;
        public static ICompilerReferenceWarning CS0728
        {
            get
            {
                if (_CS0728 == null)
                    _CS0728 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0728, 2, 728);
                return _CS0728;
            }
        }
        private static ICompilerReferenceWarning _CS0728;
        public static ICompilerReferenceWarning CS0809
        {
            get
            {
                if (_CS0809 == null)
                    _CS0809 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0809, 1, 809);
                return _CS0809;
            }
        }
        private static ICompilerReferenceWarning _CS0809;
        public static ICompilerReferenceWarning CS0824
        {
            get
            {
                if (_CS0824 == null)
                    _CS0824 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0824, 1, 824);
                return _CS0824;
            }
        }
        private static ICompilerReferenceWarning _CS0824;
        public static ICompilerReferenceWarning CS1030
        {
            get
            {
                if (_CS1030 == null)
                    _CS1030 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1030, 1, 1030);
                return _CS1030;
            }
        }
        private static ICompilerReferenceWarning _CS1030;
        public static ICompilerReferenceWarning CS1058
        {
            get
            {
                if (_CS1058 == null)
                    _CS1058 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1058, 1, 1058);
                return _CS1058;
            }
        }
        private static ICompilerReferenceWarning _CS1058;
        public static ICompilerReferenceWarning CS1060
        {
            get
            {
                if (_CS1060 == null)
                    _CS1060 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1060, 1, 1060);
                return _CS1060;
            }
        }
        private static ICompilerReferenceWarning _CS1060;
        public static ICompilerReferenceWarning CS1522
        {
            get
            {
                if (_CS1522 == null)
                    _CS1522 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1522, 1, 1522);
                return _CS1522;
            }
        }
        private static ICompilerReferenceWarning _CS1522;
        public static ICompilerReferenceWarning CS1570
        {
            get
            {
                if (_CS1570 == null)
                    _CS1570 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1570, 1, 1570);
                return _CS1570;
            }
        }
        private static ICompilerReferenceWarning _CS1570;
        public static ICompilerReferenceWarning CS1571
        {
            get
            {
                if (_CS1571 == null)
                    _CS1571 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1571, 2, 1571);
                return _CS1571;
            }
        }
        private static ICompilerReferenceWarning _CS1571;
        public static ICompilerReferenceWarning CS1572
        {
            get
            {
                if (_CS1572 == null)
                    _CS1572 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1572, 2, 1572);
                return _CS1572;
            }
        }
        private static ICompilerReferenceWarning _CS1572;
        public static ICompilerReferenceWarning CS1573
        {
            get
            {
                if (_CS1573 == null)
                    _CS1573 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1573, 4, 1573);
                return _CS1573;
            }
        }
        private static ICompilerReferenceWarning _CS1573;
        public static ICompilerReferenceWarning CS1574
        {
            get
            {
                if (_CS1574 == null)
                    _CS1574 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1574, 1, 1574);
                return _CS1574;
            }
        }
        private static ICompilerReferenceWarning _CS1574;
        public static ICompilerReferenceWarning CS1580
        {
            get
            {
                if (_CS1580 == null)
                    _CS1580 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1580, 1, 1580);
                return _CS1580;
            }
        }
        private static ICompilerReferenceWarning _CS1580;
        public static ICompilerReferenceWarning CS1581
        {
            get
            {
                if (_CS1581 == null)
                    _CS1581 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1581, 1, 1581);
                return _CS1581;
            }
        }
        private static ICompilerReferenceWarning _CS1581;
        public static ICompilerReferenceWarning CS1584
        {
            get
            {
                if (_CS1584 == null)
                    _CS1584 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1584, 1, 1584);
                return _CS1584;
            }
        }
        private static ICompilerReferenceWarning _CS1584;
        public static ICompilerReferenceWarning CS1587
        {
            get
            {
                if (_CS1587 == null)
                    _CS1587 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1587, 2, 1587);
                return _CS1587;
            }
        }
        private static ICompilerReferenceWarning _CS1587;
        public static ICompilerReferenceWarning CS1589
        {
            get
            {
                if (_CS1589 == null)
                    _CS1589 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1589, 1, 1589);
                return _CS1589;
            }
        }
        private static ICompilerReferenceWarning _CS1589;
        public static ICompilerReferenceWarning CS1590
        {
            get
            {
                if (_CS1590 == null)
                    _CS1590 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1590, 1, 1590);
                return _CS1590;
            }
        }
        private static ICompilerReferenceWarning _CS1590;
        public static ICompilerReferenceWarning CS1591
        {
            get
            {
                if (_CS1591 == null)
                    _CS1591 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1591, 4, 1591);
                return _CS1591;
            }
        }
        private static ICompilerReferenceWarning _CS1591;
        public static ICompilerReferenceWarning CS1592
        {
            get
            {
                if (_CS1592 == null)
                    _CS1592 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1592, 1, 1592);
                return _CS1592;
            }
        }
        private static ICompilerReferenceWarning _CS1592;
        public static ICompilerReferenceWarning CS1598
        {
            get
            {
                if (_CS1598 == null)
                    _CS1598 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1598, 1, 1598);
                return _CS1598;
            }
        }
        private static ICompilerReferenceWarning _CS1598;
        public static ICompilerReferenceWarning CS1607
        {
            get
            {
                if (_CS1607 == null)
                    _CS1607 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1607, 1, 1607);
                return _CS1607;
            }
        }
        private static ICompilerReferenceWarning _CS1607;
        public static ICompilerReferenceWarning CS1610
        {
            get
            {
                if (_CS1610 == null)
                    _CS1610 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1610, 4, 1610);
                return _CS1610;
            }
        }
        private static ICompilerReferenceWarning _CS1610;
        public static ICompilerReferenceWarning CS1616
        {
            get
            {
                if (_CS1616 == null)
                    _CS1616 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1616, 1, 1616);
                return _CS1616;
            }
        }
        private static ICompilerReferenceWarning _CS1616;
        public static ICompilerReferenceWarning CS1633
        {
            get
            {
                if (_CS1633 == null)
                    _CS1633 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1633, 1, 1633);
                return _CS1633;
            }
        }
        private static ICompilerReferenceWarning _CS1633;
        public static ICompilerReferenceWarning CS1634
        {
            get
            {
                if (_CS1634 == null)
                    _CS1634 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1634, 1, 1634);
                return _CS1634;
            }
        }
        private static ICompilerReferenceWarning _CS1634;
        public static ICompilerReferenceWarning CS1635
        {
            get
            {
                if (_CS1635 == null)
                    _CS1635 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1635, 1, 1635);
                return _CS1635;
            }
        }
        private static ICompilerReferenceWarning _CS1635;
        public static ICompilerReferenceWarning CS1645
        {
            get
            {
                if (_CS1645 == null)
                    _CS1645 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1645, 1, 1645);
                return _CS1645;
            }
        }
        private static ICompilerReferenceWarning _CS1645;
        public static ICompilerReferenceWarning CS1658
        {
            get
            {
                if (_CS1658 == null)
                    _CS1658 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1658, 1, 1658);
                return _CS1658;
            }
        }
        private static ICompilerReferenceWarning _CS1658;
        public static ICompilerReferenceWarning CS1668
        {
            get
            {
                if (_CS1668 == null)
                    _CS1668 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1668, 2, 1668);
                return _CS1668;
            }
        }
        private static ICompilerReferenceWarning _CS1668;
        public static ICompilerReferenceWarning CS1682
        {
            get
            {
                if (_CS1682 == null)
                    _CS1682 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1682, 1, 1682);
                return _CS1682;
            }
        }
        private static ICompilerReferenceWarning _CS1682;
        public static ICompilerReferenceWarning CS1683
        {
            get
            {
                if (_CS1683 == null)
                    _CS1683 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1683, 1, 1683);
                return _CS1683;
            }
        }
        private static ICompilerReferenceWarning _CS1683;
        public static ICompilerReferenceWarning CS1684
        {
            get
            {
                if (_CS1684 == null)
                    _CS1684 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1684, 1, 1684);
                return _CS1684;
            }
        }
        private static ICompilerReferenceWarning _CS1684;
        //System variation of CS0436
        public static ICompilerReferenceWarning CS1685
        {
            get
            {
                if (_CS1685 == null)
                    _CS1685 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1685, 1, 1685);
                return _CS1685;
            }
        }
        private static ICompilerReferenceWarning _CS1685;
        public static ICompilerReferenceWarning CS1687
        {
            get
            {
                if (_CS1687 == null)
                    _CS1687 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1687, 1, 1687);
                return _CS1687;
            }
        }
        private static ICompilerReferenceWarning _CS1687;
        public static ICompilerReferenceWarning CS1690
        {
            get
            {
                if (_CS1690 == null)
                    _CS1690 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1690, 1, 1690);
                return _CS1690;
            }
        }
        private static ICompilerReferenceWarning _CS1690;
        public static ICompilerReferenceWarning CS1691
        {
            get
            {
                if (_CS1691 == null)
                    _CS1691 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1691, 1, 1691);
                return _CS1691;
            }
        }
        private static ICompilerReferenceWarning _CS1691;
        public static ICompilerReferenceWarning CS1692
        {
            get
            {
                if (_CS1692 == null)
                    _CS1692 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1692, 1, 1692);
                return _CS1692;
            }
        }
        private static ICompilerReferenceWarning _CS1692;
        public static ICompilerReferenceWarning CS1694
        {
            get
            {
                if (_CS1694 == null)
                    _CS1694 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1694, 1, 1694);
                return _CS1694;
            }
        }
        private static ICompilerReferenceWarning _CS1694;
        public static ICompilerReferenceWarning CS1695
        {
            get
            {
                if (_CS1695 == null)
                    _CS1695 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1695, 1, 1695);
                return _CS1695;
            }
        }
        private static ICompilerReferenceWarning _CS1695;
        public static ICompilerReferenceWarning CS1696
        {
            get
            {
                if (_CS1696 == null)
                    _CS1696 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1696, 1, 1696);
                return _CS1696;
            }
        }
        private static ICompilerReferenceWarning _CS1696;
        public static ICompilerReferenceWarning CS1697
        {
            get
            {
                if (_CS1697 == null)
                    _CS1697 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1697, 1, 1697);
                return _CS1697;
            }
        }
        private static ICompilerReferenceWarning _CS1697;
        public static ICompilerReferenceWarning CS1698
        {
            get
            {
                if (_CS1698 == null)
                    _CS1698 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1698, 2, 1698);
                return _CS1698;
            }
        }
        private static ICompilerReferenceWarning _CS1698;
        public static ICompilerReferenceWarning CS1699
        {
            get
            {
                if (_CS1699 == null)
                    _CS1699 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1699, 1, 1699);
                return _CS1699;
            }
        }
        private static ICompilerReferenceWarning _CS1699;
        public static ICompilerReferenceWarning CS1700
        {
            get
            {
                if (_CS1700 == null)
                    _CS1700 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1700, 3, 1700);
                return _CS1700;
            }
        }
        private static ICompilerReferenceWarning _CS1700;
        public static ICompilerReferenceWarning CS1701
        {
            get
            {
                if (_CS1701 == null)
                    _CS1701 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1701, 2, 1701);
                return _CS1701;
            }
        }
        private static ICompilerReferenceWarning _CS1701;
        public static ICompilerReferenceWarning CS1702
        {
            get
            {
                if (_CS1702 == null)
                    _CS1702 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1702, 3, 1702);
                return _CS1702;
            }
        }
        private static ICompilerReferenceWarning _CS1702;
        public static ICompilerReferenceWarning CS1707
        {
            get
            {
                if (_CS1707 == null)
                    _CS1707 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1707, 1, 1707);
                return _CS1707;
            }
        }
        private static ICompilerReferenceWarning _CS1707;
        public static ICompilerReferenceWarning CS1709
        {
            get
            {
                if (_CS1709 == null)
                    _CS1709 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1709, 1, 1709);
                return _CS1709;
            }
        }
        private static ICompilerReferenceWarning _CS1709;
        public static ICompilerReferenceWarning CS1710
        {
            get
            {
                if (_CS1710 == null)
                    _CS1710 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1710, 2, 1710);
                return _CS1710;
            }
        }
        private static ICompilerReferenceWarning _CS1710;
        public static ICompilerReferenceWarning CS1711
        {
            get
            {
                if (_CS1711 == null)
                    _CS1711 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1711, 2, 1711);
                return _CS1711;
            }
        }
        private static ICompilerReferenceWarning _CS1711;
        public static ICompilerReferenceWarning CS1712
        {
            get
            {
                if (_CS1712 == null)
                    _CS1712 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1712, 4, 1712);
                return _CS1712;
            }
        }
        private static ICompilerReferenceWarning _CS1712;
        public static ICompilerReferenceWarning CS1717
        {
            get
            {
                if (_CS1717 == null)
                    _CS1717 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1717, 3, 1717);
                return _CS1717;
            }
        }
        private static ICompilerReferenceWarning _CS1717;
        public static ICompilerReferenceWarning CS1718
        {
            get
            {
                if (_CS1718 == null)
                    _CS1718 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1718, 3, 1718);
                return _CS1718;
            }
        }
        private static ICompilerReferenceWarning _CS1718;
        public static ICompilerReferenceWarning CS1720
        {
            get
            {
                if (_CS1720 == null)
                    _CS1720 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1720, 1, 1720);
                return _CS1720;
            }
        }
        private static ICompilerReferenceWarning _CS1720;
        public static ICompilerReferenceWarning CS1723
        {
            get
            {
                if (_CS1723 == null)
                    _CS1723 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1723, 1, 1723);
                return _CS1723;
            }
        }
        private static ICompilerReferenceWarning _CS1723;
        public static ICompilerReferenceWarning CS1911
        {
            get
            {
                if (_CS1911 == null)
                    _CS1911 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1911, 1, 1911);
                return _CS1911;
            }
        }
        private static ICompilerReferenceWarning _CS1911;
        public static ICompilerReferenceWarning CS1927
        {
            get
            {
                if (_CS1927 == null)
                    _CS1927 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1927, 2, 1927);
                return _CS1927;
            }
        }
        private static ICompilerReferenceWarning _CS1927;
        public static ICompilerReferenceWarning CS1956
        {
            get
            {
                if (_CS1956 == null)
                    _CS1956 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1956, 1, 1956);
                return _CS1956;
            }
        }
        private static ICompilerReferenceWarning _CS1956;
        public static ICompilerReferenceWarning CS1957
        {
            get
            {
                if (_CS1957 == null)
                    _CS1957 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1957, 1, 1957);
                return _CS1957;
            }
        }
        private static ICompilerReferenceWarning _CS1957;
        public static ICompilerReferenceWarning CS2002
        {
            get
            {
                if (_CS2002 == null)
                    _CS2002 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2002, 1, 2002);
                return _CS2002;
            }
        }
        private static ICompilerReferenceWarning _CS2002;
        public static ICompilerReferenceWarning CS2014
        {
            get
            {
                if (_CS2014 == null)
                    _CS2014 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2014, 1, 2014);
                return _CS2014;
            }
        }
        private static ICompilerReferenceWarning _CS2014;
        public static ICompilerReferenceWarning CS2023
        {
            get
            {
                if (_CS2023 == null)
                    _CS2023 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2023, 1, 2023);
                return _CS2023;
            }
        }
        private static ICompilerReferenceWarning _CS2023;
        public static ICompilerReferenceWarning CS2029
        {
            get
            {
                if (_CS2029 == null)
                    _CS2029 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2029, 1, 2029);
                return _CS2029;
            }
        }
        private static ICompilerReferenceWarning _CS2029;
        public static ICompilerReferenceWarning CS3000
        {
            get
            {
                if (_CS3000 == null)
                    _CS3000 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3000, 1, 3000);
                return _CS3000;
            }
        }
        private static ICompilerReferenceWarning _CS3000;
        public static ICompilerReferenceWarning CS3001
        {
            get
            {
                if (_CS3001 == null)
                    _CS3001 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3001, 1, 3001);
                return _CS3001;
            }
        }
        private static ICompilerReferenceWarning _CS3001;
        public static ICompilerReferenceWarning CS3002
        {
            get
            {
                if (_CS3002 == null)
                    _CS3002 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3002, 1, 3002);
                return _CS3002;
            }
        }
        private static ICompilerReferenceWarning _CS3002;
        public static ICompilerReferenceWarning CS3003
        {
            get
            {
                if (_CS3003 == null)
                    _CS3003 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3003, 1, 3003);
                return _CS3003;
            }
        }
        private static ICompilerReferenceWarning _CS3003;
        public static ICompilerReferenceWarning CS3004
        {
            get
            {
                if (_CS3004 == null)
                    _CS3004 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3004, 1, 3004);
                return _CS3004;
            }
        }
        private static ICompilerReferenceWarning _CS3004;
        public static ICompilerReferenceWarning CS3005
        {
            get
            {
                if (_CS3005 == null)
                    _CS3005 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3005, 1, 3005);
                return _CS3005;
            }
        }
        private static ICompilerReferenceWarning _CS3005;
        public static ICompilerReferenceWarning CS3006
        {
            get
            {
                if (_CS3006 == null)
                    _CS3006 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3006, 1, 3006);
                return _CS3006;
            }
        }
        private static ICompilerReferenceWarning _CS3006;
        public static ICompilerReferenceWarning CS3007
        {
            get
            {
                if (_CS3007 == null)
                    _CS3007 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3007, 1, 3007);
                return _CS3007;
            }
        }
        private static ICompilerReferenceWarning _CS3007;
        public static ICompilerReferenceWarning CS3008
        {
            get
            {
                if (_CS3008 == null)
                    _CS3008 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3008, 1, 3008);
                return _CS3008;
            }
        }
        private static ICompilerReferenceWarning _CS3008;
        public static ICompilerReferenceWarning CS3009
        {
            get
            {
                if (_CS3009 == null)
                    _CS3009 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3009, 1, 3009);
                return _CS3009;
            }
        }
        private static ICompilerReferenceWarning _CS3009;
        public static ICompilerReferenceWarning CS3010
        {
            get
            {
                if (_CS3010 == null)
                    _CS3010 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3010, 1, 3010);
                return _CS3010;
            }
        }
        private static ICompilerReferenceWarning _CS3010;
        public static ICompilerReferenceWarning CS3011
        {
            get
            {
                if (_CS3011 == null)
                    _CS3011 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3011, 1, 3011);
                return _CS3011;
            }
        }
        private static ICompilerReferenceWarning _CS3011;
        public static ICompilerReferenceWarning CS3012
        {
            get
            {
                if (_CS3012 == null)
                    _CS3012 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3012, 1, 3012);
                return _CS3012;
            }
        }
        private static ICompilerReferenceWarning _CS3012;
        public static ICompilerReferenceWarning CS3013
        {
            get
            {
                if (_CS3013 == null)
                    _CS3013 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3013, 1, 3013);
                return _CS3013;
            }
        }
        private static ICompilerReferenceWarning _CS3013;
        public static ICompilerReferenceWarning CS3014
        {
            get
            {
                if (_CS3014 == null)
                    _CS3014 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3014, 1, 3014);
                return _CS3014;
            }
        }
        private static ICompilerReferenceWarning _CS3014;
        public static ICompilerReferenceWarning CS3015
        {
            get
            {
                if (_CS3015 == null)
                    _CS3015 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3015, 1, 3015);
                return _CS3015;
            }
        }
        private static ICompilerReferenceWarning _CS3015;
        public static ICompilerReferenceWarning CS3016
        {
            get
            {
                if (_CS3016 == null)
                    _CS3016 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3016, 1, 3016);
                return _CS3016;
            }
        }
        private static ICompilerReferenceWarning _CS3016;
        public static ICompilerReferenceWarning CS3017
        {
            get
            {
                if (_CS3017 == null)
                    _CS3017 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3017, 1, 3017);
                return _CS3017;
            }
        }
        private static ICompilerReferenceWarning _CS3017;
        public static ICompilerReferenceWarning CS3018
        {
            get
            {
                if (_CS3018 == null)
                    _CS3018 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3018, 1, 3018);
                return _CS3018;
            }
        }
        private static ICompilerReferenceWarning _CS3018;
        public static ICompilerReferenceWarning CS3019
        {
            get
            {
                if (_CS3019 == null)
                    _CS3019 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3019, 2, 3019);
                return _CS3019;
            }
        }
        private static ICompilerReferenceWarning _CS3019;
        public static ICompilerReferenceWarning CS3021
        {
            get
            {
                if (_CS3021 == null)
                    _CS3021 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3021, 2, 3021);
                return _CS3021;
            }
        }
        private static ICompilerReferenceWarning _CS3021;
        public static ICompilerReferenceWarning CS3022
        {
            get
            {
                if (_CS3022 == null)
                    _CS3022 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3022, 1, 3022);
                return _CS3022;
            }
        }
        private static ICompilerReferenceWarning _CS3022;
        public static ICompilerReferenceWarning CS3023
        {
            get
            {
                if (_CS3023 == null)
                    _CS3023 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3023, 1, 3023);
                return _CS3023;
            }
        }
        private static ICompilerReferenceWarning _CS3023;
        public static ICompilerReferenceWarning CS3026
        {
            get
            {
                if (_CS3026 == null)
                    _CS3026 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3026, 1, 3026);
                return _CS3026;
            }
        }
        private static ICompilerReferenceWarning _CS3026;
        public static ICompilerReferenceWarning CS3027
        {
            get
            {
                if (_CS3027 == null)
                    _CS3027 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3027, 1, 3027);
                return _CS3027;
            }
        }
        private static ICompilerReferenceWarning _CS3027;
        public static ICompilerReferenceWarning CS5000
        {
            get
            {
                if (_CS5000 == null)
                    _CS5000 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS5000, 1, 5000);
                return _CS5000;
            }
        }
        private static ICompilerReferenceWarning _CS5000;
        public static ICompilerReferenceError CS0001
        {
            get
            {
                if (_CS0001 == null)
                    _CS0001 = new CompilerReferenceError(Resources.CSharpErrors_CS0001, 1);
                return _CS0001;
            }
        }
        private static ICompilerReferenceError _CS0001;
        public static ICompilerReferenceError CS0003
        {
            get
            {
                if (_CS0003 == null)
                    _CS0003 = new CompilerReferenceError(Resources.CSharpErrors_CS0003, 3);
                return _CS0003;
            }
        }
        private static ICompilerReferenceError _CS0003;
        public static ICompilerReferenceError CS0004
        {
            get
            {
                if (_CS0004 == null)
                    _CS0004 = new CompilerReferenceError(Resources.CSharpErrors_CS0004, 4);
                return _CS0004;
            }
        }
        private static ICompilerReferenceError _CS0004;
        public static ICompilerReferenceError CS0005
        {
            get
            {
                if (_CS0005 == null)
                    _CS0005 = new CompilerReferenceError(Resources.CSharpErrors_CS0005, 5);
                return _CS0005;
            }
        }
        private static ICompilerReferenceError _CS0005;
        public static ICompilerReferenceError CS0006
        {
            get
            {
                if (_CS0006 == null)
                    _CS0006 = new CompilerReferenceError(Resources.CSharpErrors_CS0006, 6);
                return _CS0006;
            }
        }
        private static ICompilerReferenceError _CS0006;
        public static ICompilerReferenceError CS0007
        {
            get
            {
                if (_CS0007 == null)
                    _CS0007 = new CompilerReferenceError(Resources.CSharpErrors_CS0007, 7);
                return _CS0007;
            }
        }
        private static ICompilerReferenceError _CS0007;
        public static ICompilerReferenceError CS0008
        {
            get
            {
                if (_CS0008 == null)
                    _CS0008 = new CompilerReferenceError(Resources.CSharpErrors_CS0008, 8);
                return _CS0008;
            }
        }
        private static ICompilerReferenceError _CS0008;
        public static ICompilerReferenceError CS0009
        {
            get
            {
                if (_CS0009 == null)
                    _CS0009 = new CompilerReferenceError(Resources.CSharpErrors_CS0009, 9);
                return _CS0009;
            }
        }
        private static ICompilerReferenceError _CS0009;
        public static ICompilerReferenceError CS0010
        {
            get
            {
                if (_CS0010 == null)
                    _CS0010 = new CompilerReferenceError(Resources.CSharpErrors_CS0010, 10);
                return _CS0010;
            }
        }
        private static ICompilerReferenceError _CS0010;
        public static ICompilerReferenceError CS0011
        {
            get
            {
                if (_CS0011 == null)
                    _CS0011 = new CompilerReferenceError(Resources.CSharpErrors_CS0011, 11);
                return _CS0011;
            }
        }
        private static ICompilerReferenceError _CS0011;
        public static ICompilerReferenceError CS0012
        {
            get
            {
                if (_CS0012 == null)
                    _CS0012 = new CompilerReferenceError(Resources.CSharpErrors_CS0012, 12);
                return _CS0012;
            }
        }
        private static ICompilerReferenceError _CS0012;
        public static ICompilerReferenceError CS0013
        {
            get
            {
                if (_CS0013 == null)
                    _CS0013 = new CompilerReferenceError(Resources.CSharpErrors_CS0013, 13);
                return _CS0013;
            }
        }
        private static ICompilerReferenceError _CS0013;
        public static ICompilerReferenceError CS0014
        {
            get
            {
                if (_CS0014 == null)
                    _CS0014 = new CompilerReferenceError(Resources.CSharpErrors_CS0014, 14);
                return _CS0014;
            }
        }
        private static ICompilerReferenceError _CS0014;
        public static ICompilerReferenceError CS0015
        {
            get
            {
                if (_CS0015 == null)
                    _CS0015 = new CompilerReferenceError(Resources.CSharpErrors_CS0015, 15);
                return _CS0015;
            }
        }
        private static ICompilerReferenceError _CS0015;
        public static ICompilerReferenceError CS0016
        {
            get
            {
                if (_CS0016 == null)
                    _CS0016 = new CompilerReferenceError(Resources.CSharpErrors_CS0016, 16);
                return _CS0016;
            }
        }
        private static ICompilerReferenceError _CS0016;
        public static ICompilerReferenceError CS0017
        {
            get
            {
                if (_CS0017 == null)
                    _CS0017 = new CompilerReferenceError(Resources.CSharpErrors_CS0017, 17);
                return _CS0017;
            }
        }
        private static ICompilerReferenceError _CS0017;
        public static ICompilerReferenceError CS0019
        {
            get
            {
                if (_CS0019 == null)
                    _CS0019 = new CompilerReferenceError(Resources.CSharpErrors_CS0019, 19);
                return _CS0019;
            }
        }
        private static ICompilerReferenceError _CS0019;
        public static ICompilerReferenceError CS0020
        {
            get
            {
                if (_CS0020 == null)
                    _CS0020 = new CompilerReferenceError(Resources.CSharpErrors_CS0020, 20);
                return _CS0020;
            }
        }
        private static ICompilerReferenceError _CS0020;
        public static ICompilerReferenceError CS0021
        {
            get
            {
                if (_CS0021 == null)
                    _CS0021 = new CompilerReferenceError(Resources.CSharpErrors_CS0021, 21);
                return _CS0021;
            }
        }
        private static ICompilerReferenceError _CS0021;
        public static ICompilerReferenceError CS0022
        {
            get
            {
                if (_CS0022 == null)
                    _CS0022 = new CompilerReferenceError(Resources.CSharpErrors_CS0022, 22);
                return _CS0022;
            }
        }
        private static ICompilerReferenceError _CS0022;
        public static ICompilerReferenceError CS0023
        {
            get
            {
                if (_CS0023 == null)
                    _CS0023 = new CompilerReferenceError(Resources.CSharpErrors_CS0023, 23);
                return _CS0023;
            }
        }
        private static ICompilerReferenceError _CS0023;
        public static ICompilerReferenceError CS0025
        {
            get
            {
                if (_CS0025 == null)
                    _CS0025 = new CompilerReferenceError(Resources.CSharpErrors_CS0025, 25);
                return _CS0025;
            }
        }
        private static ICompilerReferenceError _CS0025;
        public static ICompilerReferenceError CS0026
        {
            get
            {
                if (_CS0026 == null)
                    _CS0026 = new CompilerReferenceError(Resources.CSharpErrors_CS0026, 26);
                return _CS0026;
            }
        }
        private static ICompilerReferenceError _CS0026;
        public static ICompilerReferenceError CS0027
        {
            get
            {
                if (_CS0027 == null)
                    _CS0027 = new CompilerReferenceError(Resources.CSharpErrors_CS0027, 27);
                return _CS0027;
            }
        }
        private static ICompilerReferenceError _CS0027;
        public static ICompilerReferenceError CS0029
        {
            get
            {
                if (_CS0029 == null)
                    _CS0029 = new CompilerReferenceError(Resources.CSharpErrors_CS0029, 29);
                return _CS0029;
            }
        }
        private static ICompilerReferenceError _CS0029;
        public static ICompilerReferenceError CS0030
        {
            get
            {
                if (_CS0030 == null)
                    _CS0030 = new CompilerReferenceError(Resources.CSharpErrors_CS0030, 30);
                return _CS0030;
            }
        }
        private static ICompilerReferenceError _CS0030;
        public static ICompilerReferenceError CS0031
        {
            get
            {
                if (_CS0031 == null)
                    _CS0031 = new CompilerReferenceError(Resources.CSharpErrors_CS0031, 31);
                return _CS0031;
            }
        }
        private static ICompilerReferenceError _CS0031;
        public static ICompilerReferenceError CS0034
        {
            get
            {
                if (_CS0034 == null)
                    _CS0034 = new CompilerReferenceError(Resources.CSharpErrors_CS0034, 34);
                return _CS0034;
            }
        }
        private static ICompilerReferenceError _CS0034;
        public static ICompilerReferenceError CS0035
        {
            get
            {
                if (_CS0035 == null)
                    _CS0035 = new CompilerReferenceError(Resources.CSharpErrors_CS0035, 35);
                return _CS0035;
            }
        }
        private static ICompilerReferenceError _CS0035;
        public static ICompilerReferenceError CS0036
        {
            get
            {
                if (_CS0036 == null)
                    _CS0036 = new CompilerReferenceError(Resources.CSharpErrors_CS0036, 36);
                return _CS0036;
            }
        }
        private static ICompilerReferenceError _CS0036;
        public static ICompilerReferenceError CS0037
        {
            get
            {
                if (_CS0037 == null)
                    _CS0037 = new CompilerReferenceError(Resources.CSharpErrors_CS0037, 37);
                return _CS0037;
            }
        }
        private static ICompilerReferenceError _CS0037;
        public static ICompilerReferenceError CS0038
        {
            get
            {
                if (_CS0038 == null)
                    _CS0038 = new CompilerReferenceError(Resources.CSharpErrors_CS0038, 38);
                return _CS0038;
            }
        }
        private static ICompilerReferenceError _CS0038;
        public static ICompilerReferenceError CS0039
        {
            get
            {
                if (_CS0039 == null)
                    _CS0039 = new CompilerReferenceError(Resources.CSharpErrors_CS0039, 39);
                return _CS0039;
            }
        }
        private static ICompilerReferenceError _CS0039;
        public static ICompilerReferenceError CS0040
        {
            get
            {
                if (_CS0040 == null)
                    _CS0040 = new CompilerReferenceError(Resources.CSharpErrors_CS0040, 40);
                return _CS0040;
            }
        }
        private static ICompilerReferenceError _CS0040;
        public static ICompilerReferenceError CS0041
        {
            get
            {
                if (_CS0041 == null)
                    _CS0041 = new CompilerReferenceError(Resources.CSharpErrors_CS0041, 41);
                return _CS0041;
            }
        }
        private static ICompilerReferenceError _CS0041;
        public static ICompilerReferenceError CS0042
        {
            get
            {
                if (_CS0042 == null)
                    _CS0042 = new CompilerReferenceError(Resources.CSharpErrors_CS0042, 42);
                return _CS0042;
            }
        }
        private static ICompilerReferenceError _CS0042;
        public static ICompilerReferenceError CS0043
        {
            get
            {
                if (_CS0043 == null)
                    _CS0043 = new CompilerReferenceError(Resources.CSharpErrors_CS0043, 43);
                return _CS0043;
            }
        }
        private static ICompilerReferenceError _CS0043;
        public static ICompilerReferenceError CS0050
        {
            get
            {
                if (_CS0050 == null)
                    _CS0050 = new CompilerReferenceError(Resources.CSharpErrors_CS0050, 50);
                return _CS0050;
            }
        }
        private static ICompilerReferenceError _CS0050;
        public static ICompilerReferenceError CS0051
        {
            get
            {
                if (_CS0051 == null)
                    _CS0051 = new CompilerReferenceError(Resources.CSharpErrors_CS0051, 51);
                return _CS0051;
            }
        }
        private static ICompilerReferenceError _CS0051;
        public static ICompilerReferenceError CS0052
        {
            get
            {
                if (_CS0052 == null)
                    _CS0052 = new CompilerReferenceError(Resources.CSharpErrors_CS0052, 52);
                return _CS0052;
            }
        }
        private static ICompilerReferenceError _CS0052;
        public static ICompilerReferenceError CS0053
        {
            get
            {
                if (_CS0053 == null)
                    _CS0053 = new CompilerReferenceError(Resources.CSharpErrors_CS0053, 53);
                return _CS0053;
            }
        }
        private static ICompilerReferenceError _CS0053;
        public static ICompilerReferenceError CS0054
        {
            get
            {
                if (_CS0054 == null)
                    _CS0054 = new CompilerReferenceError(Resources.CSharpErrors_CS0054, 54);
                return _CS0054;
            }
        }
        private static ICompilerReferenceError _CS0054;
        public static ICompilerReferenceError CS0055
        {
            get
            {
                if (_CS0055 == null)
                    _CS0055 = new CompilerReferenceError(Resources.CSharpErrors_CS0055, 55);
                return _CS0055;
            }
        }
        private static ICompilerReferenceError _CS0055;
        public static ICompilerReferenceError CS0056
        {
            get
            {
                if (_CS0056 == null)
                    _CS0056 = new CompilerReferenceError(Resources.CSharpErrors_CS0056, 56);
                return _CS0056;
            }
        }
        private static ICompilerReferenceError _CS0056;
        public static ICompilerReferenceError CS0057
        {
            get
            {
                if (_CS0057 == null)
                    _CS0057 = new CompilerReferenceError(Resources.CSharpErrors_CS0057, 57);
                return _CS0057;
            }
        }
        private static ICompilerReferenceError _CS0057;
        public static ICompilerReferenceError CS0058
        {
            get
            {
                if (_CS0058 == null)
                    _CS0058 = new CompilerReferenceError(Resources.CSharpErrors_CS0058, 58);
                return _CS0058;
            }
        }
        private static ICompilerReferenceError _CS0058;
        public static ICompilerReferenceError CS0059
        {
            get
            {
                if (_CS0059 == null)
                    _CS0059 = new CompilerReferenceError(Resources.CSharpErrors_CS0059, 59);
                return _CS0059;
            }
        }
        private static ICompilerReferenceError _CS0059;
        public static ICompilerReferenceError CS0060
        {
            get
            {
                if (_CS0060 == null)
                    _CS0060 = new CompilerReferenceError(Resources.CSharpErrors_CS0060, 60);
                return _CS0060;
            }
        }
        private static ICompilerReferenceError _CS0060;
        public static ICompilerReferenceError CS0061
        {
            get
            {
                if (_CS0061 == null)
                    _CS0061 = new CompilerReferenceError(Resources.CSharpErrors_CS0061, 61);
                return _CS0061;
            }
        }
        private static ICompilerReferenceError _CS0061;
        public static ICompilerReferenceError CS0065
        {
            get
            {
                if (_CS0065 == null)
                    _CS0065 = new CompilerReferenceError(Resources.CSharpErrors_CS0065, 65);
                return _CS0065;
            }
        }
        private static ICompilerReferenceError _CS0065;
        public static ICompilerReferenceError CS0066
        {
            get
            {
                if (_CS0066 == null)
                    _CS0066 = new CompilerReferenceError(Resources.CSharpErrors_CS0066, 66);
                return _CS0066;
            }
        }
        private static ICompilerReferenceError _CS0066;
        public static ICompilerReferenceError CS0068
        {
            get
            {
                if (_CS0068 == null)
                    _CS0068 = new CompilerReferenceError(Resources.CSharpErrors_CS0068, 68);
                return _CS0068;
            }
        }
        private static ICompilerReferenceError _CS0068;
        public static ICompilerReferenceError CS0069
        {
            get
            {
                if (_CS0069 == null)
                    _CS0069 = new CompilerReferenceError(Resources.CSharpErrors_CS0069, 69);
                return _CS0069;
            }
        }
        private static ICompilerReferenceError _CS0069;
        public static ICompilerReferenceError CS0070
        {
            get
            {
                if (_CS0070 == null)
                    _CS0070 = new CompilerReferenceError(Resources.CSharpErrors_CS0070, 70);
                return _CS0070;
            }
        }
        private static ICompilerReferenceError _CS0070;
        public static ICompilerReferenceError CS0071
        {
            get
            {
                if (_CS0071 == null)
                    _CS0071 = new CompilerReferenceError(Resources.CSharpErrors_CS0071, 71);
                return _CS0071;
            }
        }
        private static ICompilerReferenceError _CS0071;
        public static ICompilerReferenceError CS0072
        {
            get
            {
                if (_CS0072 == null)
                    _CS0072 = new CompilerReferenceError(Resources.CSharpErrors_CS0072, 72);
                return _CS0072;
            }
        }
        private static ICompilerReferenceError _CS0072;
        public static ICompilerReferenceError CS0073
        {
            get
            {
                if (_CS0073 == null)
                    _CS0073 = new CompilerReferenceError(Resources.CSharpErrors_CS0073, 73);
                return _CS0073;
            }
        }
        private static ICompilerReferenceError _CS0073;
        public static ICompilerReferenceError CS0074
        {
            get
            {
                if (_CS0074 == null)
                    _CS0074 = new CompilerReferenceError(Resources.CSharpErrors_CS0074, 74);
                return _CS0074;
            }
        }
        private static ICompilerReferenceError _CS0074;
        public static ICompilerReferenceError CS0075
        {
            get
            {
                if (_CS0075 == null)
                    _CS0075 = new CompilerReferenceError(Resources.CSharpErrors_CS0075, 75);
                return _CS0075;
            }
        }
        private static ICompilerReferenceError _CS0075;
        public static ICompilerReferenceError CS0076
        {
            get
            {
                if (_CS0076 == null)
                    _CS0076 = new CompilerReferenceError(Resources.CSharpErrors_CS0076, 76);
                return _CS0076;
            }
        }
        private static ICompilerReferenceError _CS0076;
        public static ICompilerReferenceError CS0077
        {
            get
            {
                if (_CS0077 == null)
                    _CS0077 = new CompilerReferenceError(Resources.CSharpErrors_CS0077, 77);
                return _CS0077;
            }
        }
        private static ICompilerReferenceError _CS0077;
        public static ICompilerReferenceError CS0079
        {
            get
            {
                if (_CS0079 == null)
                    _CS0079 = new CompilerReferenceError(Resources.CSharpErrors_CS0079, 79);
                return _CS0079;
            }
        }
        private static ICompilerReferenceError _CS0079;
        public static ICompilerReferenceError CS0080
        {
            get
            {
                if (_CS0080 == null)
                    _CS0080 = new CompilerReferenceError(Resources.CSharpErrors_CS0080, 80);
                return _CS0080;
            }
        }
        private static ICompilerReferenceError _CS0080;
        public static ICompilerReferenceError CS0081
        {
            get
            {
                if (_CS0081 == null)
                    _CS0081 = new CompilerReferenceError(Resources.CSharpErrors_CS0081, 81);
                return _CS0081;
            }
        }
        private static ICompilerReferenceError _CS0081;
        public static ICompilerReferenceError CS0082
        {
            get
            {
                if (_CS0082 == null)
                    _CS0082 = new CompilerReferenceError(Resources.CSharpErrors_CS0082, 82);
                return _CS0082;
            }
        }
        private static ICompilerReferenceError _CS0082;
        public static ICompilerReferenceError CS0100
        {
            get
            {
                if (_CS0100 == null)
                    _CS0100 = new CompilerReferenceError(Resources.CSharpErrors_CS0100, 100);
                return _CS0100;
            }
        }
        private static ICompilerReferenceError _CS0100;
        public static ICompilerReferenceError CS0101
        {
            get
            {
                if (_CS0101 == null)
                    _CS0101 = new CompilerReferenceError(Resources.CSharpErrors_CS0101, 101);
                return _CS0101;
            }
        }
        private static ICompilerReferenceError _CS0101;
        public static ICompilerReferenceError CS0102
        {
            get
            {
                if (_CS0102 == null)
                    _CS0102 = new CompilerReferenceError(Resources.CSharpErrors_CS0102, 102);
                return _CS0102;
            }
        }
        private static ICompilerReferenceError _CS0102;
        public static ICompilerReferenceError CS0103
        {
            get
            {
                if (_CS0103 == null)
                    _CS0103 = new CompilerReferenceError(Resources.CSharpErrors_CS0103, 103);
                return _CS0103;
            }
        }
        private static ICompilerReferenceError _CS0103;
        public static ICompilerReferenceError CS0104
        {
            get
            {
                if (_CS0104 == null)
                    _CS0104 = new CompilerReferenceError(Resources.CSharpErrors_CS0104, 104);
                return _CS0104;
            }
        }
        private static ICompilerReferenceError _CS0104;
        public static ICompilerReferenceError CS0106
        {
            get
            {
                if (_CS0106 == null)
                    _CS0106 = new CompilerReferenceError(Resources.CSharpErrors_CS0106, 106);
                return _CS0106;
            }
        }
        private static ICompilerReferenceError _CS0106;
        public static ICompilerReferenceError CS0107
        {
            get
            {
                if (_CS0107 == null)
                    _CS0107 = new CompilerReferenceError(Resources.CSharpErrors_CS0107, 107);
                return _CS0107;
            }
        }
        private static ICompilerReferenceError _CS0107;
        public static ICompilerReferenceError CS0110
        {
            get
            {
                if (_CS0110 == null)
                    _CS0110 = new CompilerReferenceError(Resources.CSharpErrors_CS0110, 110);
                return _CS0110;
            }
        }
        private static ICompilerReferenceError _CS0110;
        public static ICompilerReferenceError CS0111
        {
            get
            {
                if (_CS0111 == null)
                    _CS0111 = new CompilerReferenceError(Resources.CSharpErrors_CS0111, 111);
                return _CS0111;
            }
        }
        private static ICompilerReferenceError _CS0111;
        public static ICompilerReferenceError CS0112
        {
            get
            {
                if (_CS0112 == null)
                    _CS0112 = new CompilerReferenceError(Resources.CSharpErrors_CS0112, 112);
                return _CS0112;
            }
        }
        private static ICompilerReferenceError _CS0112;
        public static ICompilerReferenceError CS0113
        {
            get
            {
                if (_CS0113 == null)
                    _CS0113 = new CompilerReferenceError(Resources.CSharpErrors_CS0113, 113);
                return _CS0113;
            }
        }
        private static ICompilerReferenceError _CS0113;
        public static ICompilerReferenceError CS0115
        {
            get
            {
                if (_CS0115 == null)
                    _CS0115 = new CompilerReferenceError(Resources.CSharpErrors_CS0115, 115);
                return _CS0115;
            }
        }
        private static ICompilerReferenceError _CS0115;
        public static ICompilerReferenceError CS0116
        {
            get
            {
                if (_CS0116 == null)
                    _CS0116 = new CompilerReferenceError(Resources.CSharpErrors_CS0116, 116);
                return _CS0116;
            }
        }
        private static ICompilerReferenceError _CS0116;
        public static ICompilerReferenceError CS0117
        {
            get
            {
                if (_CS0117 == null)
                    _CS0117 = new CompilerReferenceError(Resources.CSharpErrors_CS0117, 117);
                return _CS0117;
            }
        }
        private static ICompilerReferenceError _CS0117;
        public static ICompilerReferenceError CS0118
        {
            get
            {
                if (_CS0118 == null)
                    _CS0118 = new CompilerReferenceError(Resources.CSharpErrors_CS0118, 118);
                return _CS0118;
            }
        }
        private static ICompilerReferenceError _CS0118;
        public static ICompilerReferenceError CS0119
        {
            get
            {
                if (_CS0119 == null)
                    _CS0119 = new CompilerReferenceError(Resources.CSharpErrors_CS0119, 119);
                return _CS0119;
            }
        }
        private static ICompilerReferenceError _CS0119;
        public static ICompilerReferenceError CS0120
        {
            get
            {
                if (_CS0120 == null)
                    _CS0120 = new CompilerReferenceError(Resources.CSharpErrors_CS0120, 120);
                return _CS0120;
            }
        }
        private static ICompilerReferenceError _CS0120;
        public static ICompilerReferenceError CS0121
        {
            get
            {
                if (_CS0121 == null)
                    _CS0121 = new CompilerReferenceError(Resources.CSharpErrors_CS0121, 121);
                return _CS0121;
            }
        }
        private static ICompilerReferenceError _CS0121;
        public static ICompilerReferenceError CS0122
        {
            get
            {
                if (_CS0122 == null)
                    _CS0122 = new CompilerReferenceError(Resources.CSharpErrors_CS0122, 122);
                return _CS0122;
            }
        }
        private static ICompilerReferenceError _CS0122;
        public static ICompilerReferenceError CS0123
        {
            get
            {
                if (_CS0123 == null)
                    _CS0123 = new CompilerReferenceError(Resources.CSharpErrors_CS0123, 123);
                return _CS0123;
            }
        }
        private static ICompilerReferenceError _CS0123;
        public static ICompilerReferenceError CS0126
        {
            get
            {
                if (_CS0126 == null)
                    _CS0126 = new CompilerReferenceError(Resources.CSharpErrors_CS0126, 126);
                return _CS0126;
            }
        }
        private static ICompilerReferenceError _CS0126;
        public static ICompilerReferenceError CS0127
        {
            get
            {
                if (_CS0127 == null)
                    _CS0127 = new CompilerReferenceError(Resources.CSharpErrors_CS0127, 127);
                return _CS0127;
            }
        }
        private static ICompilerReferenceError _CS0127;
        public static ICompilerReferenceError CS0128
        {
            get
            {
                if (_CS0128 == null)
                    _CS0128 = new CompilerReferenceError(Resources.CSharpErrors_CS0128, 128);
                return _CS0128;
            }
        }
        private static ICompilerReferenceError _CS0128;
        public static ICompilerReferenceError CS0131
        {
            get
            {
                if (_CS0131 == null)
                    _CS0131 = new CompilerReferenceError(Resources.CSharpErrors_CS0131, 131);
                return _CS0131;
            }
        }
        private static ICompilerReferenceError _CS0131;
        public static ICompilerReferenceError CS0132
        {
            get
            {
                if (_CS0132 == null)
                    _CS0132 = new CompilerReferenceError(Resources.CSharpErrors_CS0132, 132);
                return _CS0132;
            }
        }
        private static ICompilerReferenceError _CS0132;
        public static ICompilerReferenceError CS0133
        {
            get
            {
                if (_CS0133 == null)
                    _CS0133 = new CompilerReferenceError(Resources.CSharpErrors_CS0133, 133);
                return _CS0133;
            }
        }
        private static ICompilerReferenceError _CS0133;
        public static ICompilerReferenceError CS0134
        {
            get
            {
                if (_CS0134 == null)
                    _CS0134 = new CompilerReferenceError(Resources.CSharpErrors_CS0134, 134);
                return _CS0134;
            }
        }
        private static ICompilerReferenceError _CS0134;
        public static ICompilerReferenceError CS0135
        {
            get
            {
                if (_CS0135 == null)
                    _CS0135 = new CompilerReferenceError(Resources.CSharpErrors_CS0135, 135);
                return _CS0135;
            }
        }
        private static ICompilerReferenceError _CS0135;
        public static ICompilerReferenceError CS0136
        {
            get
            {
                if (_CS0136 == null)
                    _CS0136 = new CompilerReferenceError(Resources.CSharpErrors_CS0136, 136);
                return _CS0136;
            }
        }
        private static ICompilerReferenceError _CS0136;
        public static ICompilerReferenceError CS0138
        {
            get
            {
                if (_CS0138 == null)
                    _CS0138 = new CompilerReferenceError(Resources.CSharpErrors_CS0138, 138);
                return _CS0138;
            }
        }
        private static ICompilerReferenceError _CS0138;
        public static ICompilerReferenceError CS0139
        {
            get
            {
                if (_CS0139 == null)
                    _CS0139 = new CompilerReferenceError(Resources.CSharpErrors_CS0139, 139);
                return _CS0139;
            }
        }
        private static ICompilerReferenceError _CS0139;
        public static ICompilerReferenceError CS0140
        {
            get
            {
                if (_CS0140 == null)
                    _CS0140 = new CompilerReferenceError(Resources.CSharpErrors_CS0140, 140);
                return _CS0140;
            }
        }
        private static ICompilerReferenceError _CS0140;
        public static ICompilerReferenceError CS0143
        {
            get
            {
                if (_CS0143 == null)
                    _CS0143 = new CompilerReferenceError(Resources.CSharpErrors_CS0143, 143);
                return _CS0143;
            }
        }
        private static ICompilerReferenceError _CS0143;
        public static ICompilerReferenceError CS0144
        {
            get
            {
                if (_CS0144 == null)
                    _CS0144 = new CompilerReferenceError(Resources.CSharpErrors_CS0144, 144);
                return _CS0144;
            }
        }
        private static ICompilerReferenceError _CS0144;
        public static ICompilerReferenceError CS0145
        {
            get
            {
                if (_CS0145 == null)
                    _CS0145 = new CompilerReferenceError(Resources.CSharpErrors_CS0145, 145);
                return _CS0145;
            }
        }
        private static ICompilerReferenceError _CS0145;
        public static ICompilerReferenceError CS0146
        {
            get
            {
                if (_CS0146 == null)
                    _CS0146 = new CompilerReferenceError(Resources.CSharpErrors_CS0146, 146);
                return _CS0146;
            }
        }
        private static ICompilerReferenceError _CS0146;
        public static ICompilerReferenceError CS0148
        {
            get
            {
                if (_CS0148 == null)
                    _CS0148 = new CompilerReferenceError(Resources.CSharpErrors_CS0148, 148);
                return _CS0148;
            }
        }
        private static ICompilerReferenceError _CS0148;
        public static ICompilerReferenceError CS0149
        {
            get
            {
                if (_CS0149 == null)
                    _CS0149 = new CompilerReferenceError(Resources.CSharpErrors_CS0149, 149);
                return _CS0149;
            }
        }
        private static ICompilerReferenceError _CS0149;
        public static ICompilerReferenceError CS0150
        {
            get
            {
                if (_CS0150 == null)
                    _CS0150 = new CompilerReferenceError(Resources.CSharpErrors_CS0150, 150);
                return _CS0150;
            }
        }
        private static ICompilerReferenceError _CS0150;
        public static ICompilerReferenceError CS0151
        {
            get
            {
                if (_CS0151 == null)
                    _CS0151 = new CompilerReferenceError(Resources.CSharpErrors_CS0151, 151);
                return _CS0151;
            }
        }
        private static ICompilerReferenceError _CS0151;
        public static ICompilerReferenceError CS0152
        {
            get
            {
                if (_CS0152 == null)
                    _CS0152 = new CompilerReferenceError(Resources.CSharpErrors_CS0152, 152);
                return _CS0152;
            }
        }
        private static ICompilerReferenceError _CS0152;
        public static ICompilerReferenceError CS0153
        {
            get
            {
                if (_CS0153 == null)
                    _CS0153 = new CompilerReferenceError(Resources.CSharpErrors_CS0153, 153);
                return _CS0153;
            }
        }
        private static ICompilerReferenceError _CS0153;
        public static ICompilerReferenceError CS0154
        {
            get
            {
                if (_CS0154 == null)
                    _CS0154 = new CompilerReferenceError(Resources.CSharpErrors_CS0154, 154);
                return _CS0154;
            }
        }
        private static ICompilerReferenceError _CS0154;
        public static ICompilerReferenceError CS0155
        {
            get
            {
                if (_CS0155 == null)
                    _CS0155 = new CompilerReferenceError(Resources.CSharpErrors_CS0155, 155);
                return _CS0155;
            }
        }
        private static ICompilerReferenceError _CS0155;
        public static ICompilerReferenceError CS0156
        {
            get
            {
                if (_CS0156 == null)
                    _CS0156 = new CompilerReferenceError(Resources.CSharpErrors_CS0156, 156);
                return _CS0156;
            }
        }
        private static ICompilerReferenceError _CS0156;
        public static ICompilerReferenceError CS0157
        {
            get
            {
                if (_CS0157 == null)
                    _CS0157 = new CompilerReferenceError(Resources.CSharpErrors_CS0157, 157);
                return _CS0157;
            }
        }
        private static ICompilerReferenceError _CS0157;
        public static ICompilerReferenceError CS0158
        {
            get
            {
                if (_CS0158 == null)
                    _CS0158 = new CompilerReferenceError(Resources.CSharpErrors_CS0158, 158);
                return _CS0158;
            }
        }
        private static ICompilerReferenceError _CS0158;
        public static ICompilerReferenceError CS0159
        {
            get
            {
                if (_CS0159 == null)
                    _CS0159 = new CompilerReferenceError(Resources.CSharpErrors_CS0159, 159);
                return _CS0159;
            }
        }
        private static ICompilerReferenceError _CS0159;
        public static ICompilerReferenceError CS0160
        {
            get
            {
                if (_CS0160 == null)
                    _CS0160 = new CompilerReferenceError(Resources.CSharpErrors_CS0160, 160);
                return _CS0160;
            }
        }
        private static ICompilerReferenceError _CS0160;
        public static ICompilerReferenceError CS0161
        {
            get
            {
                if (_CS0161 == null)
                    _CS0161 = new CompilerReferenceError(Resources.CSharpErrors_CS0161, 161);
                return _CS0161;
            }
        }
        private static ICompilerReferenceError _CS0161;
        public static ICompilerReferenceError CS0163
        {
            get
            {
                if (_CS0163 == null)
                    _CS0163 = new CompilerReferenceError(Resources.CSharpErrors_CS0163, 163);
                return _CS0163;
            }
        }
        private static ICompilerReferenceError _CS0163;
        public static ICompilerReferenceError CS0165
        {
            get
            {
                if (_CS0165 == null)
                    _CS0165 = new CompilerReferenceError(Resources.CSharpErrors_CS0165, 165);
                return _CS0165;
            }
        }
        private static ICompilerReferenceError _CS0165;
        public static ICompilerReferenceError CS0167
        {
            get
            {
                if (_CS0167 == null)
                    _CS0167 = new CompilerReferenceError(Resources.CSharpErrors_CS0167, 167);
                return _CS0167;
            }
        }
        private static ICompilerReferenceError _CS0167;
        public static ICompilerReferenceError CS0170
        {
            get
            {
                if (_CS0170 == null)
                    _CS0170 = new CompilerReferenceError(Resources.CSharpErrors_CS0170, 170);
                return _CS0170;
            }
        }
        private static ICompilerReferenceError _CS0170;
        public static ICompilerReferenceError CS0171
        {
            get
            {
                if (_CS0171 == null)
                    _CS0171 = new CompilerReferenceError(Resources.CSharpErrors_CS0171, 171);
                return _CS0171;
            }
        }
        private static ICompilerReferenceError _CS0171;
        public static ICompilerReferenceError CS0172
        {
            get
            {
                if (_CS0172 == null)
                    _CS0172 = new CompilerReferenceError(Resources.CSharpErrors_CS0172, 172);
                return _CS0172;
            }
        }
        private static ICompilerReferenceError _CS0172;
        public static ICompilerReferenceError CS0173
        {
            get
            {
                if (_CS0173 == null)
                    _CS0173 = new CompilerReferenceError(Resources.CSharpErrors_CS0173, 173);
                return _CS0173;
            }
        }
        private static ICompilerReferenceError _CS0173;
        public static ICompilerReferenceError CS0174
        {
            get
            {
                if (_CS0174 == null)
                    _CS0174 = new CompilerReferenceError(Resources.CSharpErrors_CS0174, 174);
                return _CS0174;
            }
        }
        private static ICompilerReferenceError _CS0174;
        public static ICompilerReferenceError CS0175
        {
            get
            {
                if (_CS0175 == null)
                    _CS0175 = new CompilerReferenceError(Resources.CSharpErrors_CS0175, 175);
                return _CS0175;
            }
        }
        private static ICompilerReferenceError _CS0175;
        public static ICompilerReferenceError CS0176
        {
            get
            {
                if (_CS0176 == null)
                    _CS0176 = new CompilerReferenceError(Resources.CSharpErrors_CS0176, 176);
                return _CS0176;
            }
        }
        private static ICompilerReferenceError _CS0176;
        public static ICompilerReferenceError CS0177
        {
            get
            {
                if (_CS0177 == null)
                    _CS0177 = new CompilerReferenceError(Resources.CSharpErrors_CS0177, 177);
                return _CS0177;
            }
        }
        private static ICompilerReferenceError _CS0177;
        public static ICompilerReferenceError CS0178
        {
            get
            {
                if (_CS0178 == null)
                    _CS0178 = new CompilerReferenceError(Resources.CSharpErrors_CS0178, 178);
                return _CS0178;
            }
        }
        private static ICompilerReferenceError _CS0178;
        public static ICompilerReferenceError CS0179
        {
            get
            {
                if (_CS0179 == null)
                    _CS0179 = new CompilerReferenceError(Resources.CSharpErrors_CS0179, 179);
                return _CS0179;
            }
        }
        private static ICompilerReferenceError _CS0179;
        public static ICompilerReferenceError CS0180
        {
            get
            {
                if (_CS0180 == null)
                    _CS0180 = new CompilerReferenceError(Resources.CSharpErrors_CS0180, 180);
                return _CS0180;
            }
        }
        private static ICompilerReferenceError _CS0180;
        public static ICompilerReferenceError CS0182
        {
            get
            {
                if (_CS0182 == null)
                    _CS0182 = new CompilerReferenceError(Resources.CSharpErrors_CS0182, 182);
                return _CS0182;
            }
        }
        private static ICompilerReferenceError _CS0182;
        public static ICompilerReferenceError CS0185
        {
            get
            {
                if (_CS0185 == null)
                    _CS0185 = new CompilerReferenceError(Resources.CSharpErrors_CS0185, 185);
                return _CS0185;
            }
        }
        private static ICompilerReferenceError _CS0185;
        public static ICompilerReferenceError CS0186
        {
            get
            {
                if (_CS0186 == null)
                    _CS0186 = new CompilerReferenceError(Resources.CSharpErrors_CS0186, 186);
                return _CS0186;
            }
        }
        private static ICompilerReferenceError _CS0186;
        public static ICompilerReferenceError CS0188
        {
            get
            {
                if (_CS0188 == null)
                    _CS0188 = new CompilerReferenceError(Resources.CSharpErrors_CS0188, 188);
                return _CS0188;
            }
        }
        private static ICompilerReferenceError _CS0188;
        public static ICompilerReferenceError CS0191
        {
            get
            {
                if (_CS0191 == null)
                    _CS0191 = new CompilerReferenceError(Resources.CSharpErrors_CS0191, 191);
                return _CS0191;
            }
        }
        private static ICompilerReferenceError _CS0191;
        public static ICompilerReferenceError CS0192
        {
            get
            {
                if (_CS0192 == null)
                    _CS0192 = new CompilerReferenceError(Resources.CSharpErrors_CS0192, 192);
                return _CS0192;
            }
        }
        private static ICompilerReferenceError _CS0192;
        public static ICompilerReferenceError CS0193
        {
            get
            {
                if (_CS0193 == null)
                    _CS0193 = new CompilerReferenceError(Resources.CSharpErrors_CS0193, 193);
                return _CS0193;
            }
        }
        private static ICompilerReferenceError _CS0193;
        public static ICompilerReferenceError CS0196
        {
            get
            {
                if (_CS0196 == null)
                    _CS0196 = new CompilerReferenceError(Resources.CSharpErrors_CS0196, 196);
                return _CS0196;
            }
        }
        private static ICompilerReferenceError _CS0196;
        public static ICompilerReferenceError CS0198
        {
            get
            {
                if (_CS0198 == null)
                    _CS0198 = new CompilerReferenceError(Resources.CSharpErrors_CS0198, 198);
                return _CS0198;
            }
        }
        private static ICompilerReferenceError _CS0198;
        public static ICompilerReferenceError CS0199
        {
            get
            {
                if (_CS0199 == null)
                    _CS0199 = new CompilerReferenceError(Resources.CSharpErrors_CS0199, 199);
                return _CS0199;
            }
        }
        private static ICompilerReferenceError _CS0199;
        public static ICompilerReferenceError CS0200
        {
            get
            {
                if (_CS0200 == null)
                    _CS0200 = new CompilerReferenceError(Resources.CSharpErrors_CS0200, 200);
                return _CS0200;
            }
        }
        private static ICompilerReferenceError _CS0200;
        public static ICompilerReferenceError CS0201
        {
            get
            {
                if (_CS0201 == null)
                    _CS0201 = new CompilerReferenceError(Resources.CSharpErrors_CS0201, 201);
                return _CS0201;
            }
        }
        private static ICompilerReferenceError _CS0201;
        public static ICompilerReferenceError CS0202
        {
            get
            {
                if (_CS0202 == null)
                    _CS0202 = new CompilerReferenceError(Resources.CSharpErrors_CS0202, 202);
                return _CS0202;
            }
        }
        private static ICompilerReferenceError _CS0202;
        public static ICompilerReferenceError CS0204
        {
            get
            {
                if (_CS0204 == null)
                    _CS0204 = new CompilerReferenceError(Resources.CSharpErrors_CS0204, 204);
                return _CS0204;
            }
        }
        private static ICompilerReferenceError _CS0204;
        public static ICompilerReferenceError CS0205
        {
            get
            {
                if (_CS0205 == null)
                    _CS0205 = new CompilerReferenceError(Resources.CSharpErrors_CS0205, 205);
                return _CS0205;
            }
        }
        private static ICompilerReferenceError _CS0205;
        public static ICompilerReferenceError CS0206
        {
            get
            {
                if (_CS0206 == null)
                    _CS0206 = new CompilerReferenceError(Resources.CSharpErrors_CS0206, 206);
                return _CS0206;
            }
        }
        private static ICompilerReferenceError _CS0206;
        public static ICompilerReferenceError CS0208
        {
            get
            {
                if (_CS0208 == null)
                    _CS0208 = new CompilerReferenceError(Resources.CSharpErrors_CS0208, 208);
                return _CS0208;
            }
        }
        private static ICompilerReferenceError _CS0208;
        public static ICompilerReferenceError CS0209
        {
            get
            {
                if (_CS0209 == null)
                    _CS0209 = new CompilerReferenceError(Resources.CSharpErrors_CS0209, 209);
                return _CS0209;
            }
        }
        private static ICompilerReferenceError _CS0209;
        public static ICompilerReferenceError CS0210
        {
            get
            {
                if (_CS0210 == null)
                    _CS0210 = new CompilerReferenceError(Resources.CSharpErrors_CS0210, 210);
                return _CS0210;
            }
        }
        private static ICompilerReferenceError _CS0210;
        public static ICompilerReferenceError CS0211
        {
            get
            {
                if (_CS0211 == null)
                    _CS0211 = new CompilerReferenceError(Resources.CSharpErrors_CS0211, 211);
                return _CS0211;
            }
        }
        private static ICompilerReferenceError _CS0211;
        public static ICompilerReferenceError CS0212
        {
            get
            {
                if (_CS0212 == null)
                    _CS0212 = new CompilerReferenceError(Resources.CSharpErrors_CS0212, 212);
                return _CS0212;
            }
        }
        private static ICompilerReferenceError _CS0212;
        public static ICompilerReferenceError CS0213
        {
            get
            {
                if (_CS0213 == null)
                    _CS0213 = new CompilerReferenceError(Resources.CSharpErrors_CS0213, 213);
                return _CS0213;
            }
        }
        private static ICompilerReferenceError _CS0213;
        public static ICompilerReferenceError CS0214
        {
            get
            {
                if (_CS0214 == null)
                    _CS0214 = new CompilerReferenceError(Resources.CSharpErrors_CS0214, 214);
                return _CS0214;
            }
        }
        private static ICompilerReferenceError _CS0214;
        public static ICompilerReferenceError CS0215
        {
            get
            {
                if (_CS0215 == null)
                    _CS0215 = new CompilerReferenceError(Resources.CSharpErrors_CS0215, 215);
                return _CS0215;
            }
        }
        private static ICompilerReferenceError _CS0215;
        public static ICompilerReferenceError CS0216
        {
            get
            {
                if (_CS0216 == null)
                    _CS0216 = new CompilerReferenceError(Resources.CSharpErrors_CS0216, 216);
                return _CS0216;
            }
        }
        private static ICompilerReferenceError _CS0216;
        public static ICompilerReferenceError CS0217
        {
            get
            {
                if (_CS0217 == null)
                    _CS0217 = new CompilerReferenceError(Resources.CSharpErrors_CS0217, 217);
                return _CS0217;
            }
        }
        private static ICompilerReferenceError _CS0217;
        public static ICompilerReferenceError CS0218
        {
            get
            {
                if (_CS0218 == null)
                    _CS0218 = new CompilerReferenceError(Resources.CSharpErrors_CS0218, 218);
                return _CS0218;
            }
        }
        private static ICompilerReferenceError _CS0218;
        public static ICompilerReferenceError CS0220
        {
            get
            {
                if (_CS0220 == null)
                    _CS0220 = new CompilerReferenceError(Resources.CSharpErrors_CS0220, 220);
                return _CS0220;
            }
        }
        private static ICompilerReferenceError _CS0220;
        public static ICompilerReferenceError CS0221
        {
            get
            {
                if (_CS0221 == null)
                    _CS0221 = new CompilerReferenceError(Resources.CSharpErrors_CS0221, 221);
                return _CS0221;
            }
        }
        private static ICompilerReferenceError _CS0221;
        public static ICompilerReferenceError CS0225
        {
            get
            {
                if (_CS0225 == null)
                    _CS0225 = new CompilerReferenceError(Resources.CSharpErrors_CS0225, 225);
                return _CS0225;
            }
        }
        private static ICompilerReferenceError _CS0225;
        public static ICompilerReferenceError CS0226
        {
            get
            {
                if (_CS0226 == null)
                    _CS0226 = new CompilerReferenceError(Resources.CSharpErrors_CS0226, 226);
                return _CS0226;
            }
        }
        private static ICompilerReferenceError _CS0226;
        public static ICompilerReferenceError CS0227
        {
            get
            {
                if (_CS0227 == null)
                    _CS0227 = new CompilerReferenceError(Resources.CSharpErrors_CS0227, 227);
                return _CS0227;
            }
        }
        private static ICompilerReferenceError _CS0227;
        public static ICompilerReferenceError CS0228
        {
            get
            {
                if (_CS0228 == null)
                    _CS0228 = new CompilerReferenceError(Resources.CSharpErrors_CS0228, 228);
                return _CS0228;
            }
        }
        private static ICompilerReferenceError _CS0228;
        public static ICompilerReferenceError CS0229
        {
            get
            {
                if (_CS0229 == null)
                    _CS0229 = new CompilerReferenceError(Resources.CSharpErrors_CS0229, 229);
                return _CS0229;
            }
        }
        private static ICompilerReferenceError _CS0229;
        public static ICompilerReferenceError CS0230
        {
            get
            {
                if (_CS0230 == null)
                    _CS0230 = new CompilerReferenceError(Resources.CSharpErrors_CS0230, 230);
                return _CS0230;
            }
        }
        private static ICompilerReferenceError _CS0230;
        public static ICompilerReferenceError CS0231
        {
            get
            {
                if (_CS0231 == null)
                    _CS0231 = new CompilerReferenceError(Resources.CSharpErrors_CS0231, 231);
                return _CS0231;
            }
        }
        private static ICompilerReferenceError _CS0231;
        public static ICompilerReferenceError CS0233
        {
            get
            {
                if (_CS0233 == null)
                    _CS0233 = new CompilerReferenceError(Resources.CSharpErrors_CS0233, 233);
                return _CS0233;
            }
        }
        private static ICompilerReferenceError _CS0233;
        public static ICompilerReferenceError CS0234
        {
            get
            {
                if (_CS0234 == null)
                    _CS0234 = new CompilerReferenceError(Resources.CSharpErrors_CS0234, 234);
                return _CS0234;
            }
        }
        private static ICompilerReferenceError _CS0234;
        public static ICompilerReferenceError CS0236
        {
            get
            {
                if (_CS0236 == null)
                    _CS0236 = new CompilerReferenceError(Resources.CSharpErrors_CS0236, 236);
                return _CS0236;
            }
        }
        private static ICompilerReferenceError _CS0236;
        public static ICompilerReferenceError CS0238
        {
            get
            {
                if (_CS0238 == null)
                    _CS0238 = new CompilerReferenceError(Resources.CSharpErrors_CS0238, 238);
                return _CS0238;
            }
        }
        private static ICompilerReferenceError _CS0238;
        public static ICompilerReferenceError CS0239
        {
            get
            {
                if (_CS0239 == null)
                    _CS0239 = new CompilerReferenceError(Resources.CSharpErrors_CS0239, 239);
                return _CS0239;
            }
        }
        private static ICompilerReferenceError _CS0239;
        public static ICompilerReferenceError CS0241
        {
            get
            {
                if (_CS0241 == null)
                    _CS0241 = new CompilerReferenceError(Resources.CSharpErrors_CS0241, 241);
                return _CS0241;
            }
        }
        private static ICompilerReferenceError _CS0241;
        public static ICompilerReferenceError CS0242
        {
            get
            {
                if (_CS0242 == null)
                    _CS0242 = new CompilerReferenceError(Resources.CSharpErrors_CS0242, 242);
                return _CS0242;
            }
        }
        private static ICompilerReferenceError _CS0242;
        public static ICompilerReferenceError CS0243
        {
            get
            {
                if (_CS0243 == null)
                    _CS0243 = new CompilerReferenceError(Resources.CSharpErrors_CS0243, 243);
                return _CS0243;
            }
        }
        private static ICompilerReferenceError _CS0243;
        public static ICompilerReferenceError CS0244
        {
            get
            {
                if (_CS0244 == null)
                    _CS0244 = new CompilerReferenceError(Resources.CSharpErrors_CS0244, 244);
                return _CS0244;
            }
        }
        private static ICompilerReferenceError _CS0244;
        public static ICompilerReferenceError CS0245
        {
            get
            {
                if (_CS0245 == null)
                    _CS0245 = new CompilerReferenceError(Resources.CSharpErrors_CS0245, 245);
                return _CS0245;
            }
        }
        private static ICompilerReferenceError _CS0245;
        public static ICompilerReferenceError CS0246
        {
            get
            {
                if (_CS0246 == null)
                    _CS0246 = new CompilerReferenceError(Resources.CSharpErrors_CS0246, 246);
                return _CS0246;
            }
        }
        private static ICompilerReferenceError _CS0246;
        public static ICompilerReferenceError CS0247
        {
            get
            {
                if (_CS0247 == null)
                    _CS0247 = new CompilerReferenceError(Resources.CSharpErrors_CS0247, 247);
                return _CS0247;
            }
        }
        private static ICompilerReferenceError _CS0247;
        public static ICompilerReferenceError CS0248
        {
            get
            {
                if (_CS0248 == null)
                    _CS0248 = new CompilerReferenceError(Resources.CSharpErrors_CS0248, 248);
                return _CS0248;
            }
        }
        private static ICompilerReferenceError _CS0248;
        public static ICompilerReferenceError CS0249
        {
            get
            {
                if (_CS0249 == null)
                    _CS0249 = new CompilerReferenceError(Resources.CSharpErrors_CS0249, 249);
                return _CS0249;
            }
        }
        private static ICompilerReferenceError _CS0249;
        public static ICompilerReferenceError CS0250
        {
            get
            {
                if (_CS0250 == null)
                    _CS0250 = new CompilerReferenceError(Resources.CSharpErrors_CS0250, 250);
                return _CS0250;
            }
        }
        private static ICompilerReferenceError _CS0250;
        public static ICompilerReferenceError CS0254
        {
            get
            {
                if (_CS0254 == null)
                    _CS0254 = new CompilerReferenceError(Resources.CSharpErrors_CS0254, 254);
                return _CS0254;
            }
        }
        private static ICompilerReferenceError _CS0254;
        public static ICompilerReferenceError CS0255
        {
            get
            {
                if (_CS0255 == null)
                    _CS0255 = new CompilerReferenceError(Resources.CSharpErrors_CS0255, 255);
                return _CS0255;
            }
        }
        private static ICompilerReferenceError _CS0255;
        public static ICompilerReferenceError CS0260
        {
            get
            {
                if (_CS0260 == null)
                    _CS0260 = new CompilerReferenceError(Resources.CSharpErrors_CS0260, 260);
                return _CS0260;
            }
        }
        private static ICompilerReferenceError _CS0260;
        public static ICompilerReferenceError CS0261
        {
            get
            {
                if (_CS0261 == null)
                    _CS0261 = new CompilerReferenceError(Resources.CSharpErrors_CS0261, 261);
                return _CS0261;
            }
        }
        private static ICompilerReferenceError _CS0261;
        public static ICompilerReferenceError CS0262
        {
            get
            {
                if (_CS0262 == null)
                    _CS0262 = new CompilerReferenceError(Resources.CSharpErrors_CS0262, 262);
                return _CS0262;
            }
        }
        private static ICompilerReferenceError _CS0262;
        public static ICompilerReferenceError CS0263
        {
            get
            {
                if (_CS0263 == null)
                    _CS0263 = new CompilerReferenceError(Resources.CSharpErrors_CS0263, 263);
                return _CS0263;
            }
        }
        private static ICompilerReferenceError _CS0263;
        public static ICompilerReferenceError CS0264
        {
            get
            {
                if (_CS0264 == null)
                    _CS0264 = new CompilerReferenceError(Resources.CSharpErrors_CS0264, 264);
                return _CS0264;
            }
        }
        private static ICompilerReferenceError _CS0264;
        public static ICompilerReferenceError CS0265
        {
            get
            {
                if (_CS0265 == null)
                    _CS0265 = new CompilerReferenceError(Resources.CSharpErrors_CS0265, 265);
                return _CS0265;
            }
        }
        private static ICompilerReferenceError _CS0265;
        public static ICompilerReferenceError CS0266
        {
            get
            {
                if (_CS0266 == null)
                    _CS0266 = new CompilerReferenceError(Resources.CSharpErrors_CS0266, 266);
                return _CS0266;
            }
        }
        private static ICompilerReferenceError _CS0266;
        public static ICompilerReferenceError CS0267
        {
            get
            {
                if (_CS0267 == null)
                    _CS0267 = new CompilerReferenceError(Resources.CSharpErrors_CS0267, 267);
                return _CS0267;
            }
        }
        private static ICompilerReferenceError _CS0267;
        public static ICompilerReferenceError CS0268
        {
            get
            {
                if (_CS0268 == null)
                    _CS0268 = new CompilerReferenceError(Resources.CSharpErrors_CS0268, 268);
                return _CS0268;
            }
        }
        private static ICompilerReferenceError _CS0268;
        public static ICompilerReferenceError CS0269
        {
            get
            {
                if (_CS0269 == null)
                    _CS0269 = new CompilerReferenceError(Resources.CSharpErrors_CS0269, 269);
                return _CS0269;
            }
        }
        private static ICompilerReferenceError _CS0269;
        public static ICompilerReferenceError CS0270
        {
            get
            {
                if (_CS0270 == null)
                    _CS0270 = new CompilerReferenceError(Resources.CSharpErrors_CS0270, 270);
                return _CS0270;
            }
        }
        private static ICompilerReferenceError _CS0270;
        public static ICompilerReferenceError CS0271
        {
            get
            {
                if (_CS0271 == null)
                    _CS0271 = new CompilerReferenceError(Resources.CSharpErrors_CS0271, 271);
                return _CS0271;
            }
        }
        private static ICompilerReferenceError _CS0271;
        public static ICompilerReferenceError CS0272
        {
            get
            {
                if (_CS0272 == null)
                    _CS0272 = new CompilerReferenceError(Resources.CSharpErrors_CS0272, 272);
                return _CS0272;
            }
        }
        private static ICompilerReferenceError _CS0272;
        public static ICompilerReferenceError CS0273
        {
            get
            {
                if (_CS0273 == null)
                    _CS0273 = new CompilerReferenceError(Resources.CSharpErrors_CS0273, 273);
                return _CS0273;
            }
        }
        private static ICompilerReferenceError _CS0273;
        public static ICompilerReferenceError CS0274
        {
            get
            {
                if (_CS0274 == null)
                    _CS0274 = new CompilerReferenceError(Resources.CSharpErrors_CS0274, 274);
                return _CS0274;
            }
        }
        private static ICompilerReferenceError _CS0274;
        public static ICompilerReferenceError CS0275
        {
            get
            {
                if (_CS0275 == null)
                    _CS0275 = new CompilerReferenceError(Resources.CSharpErrors_CS0275, 275);
                return _CS0275;
            }
        }
        private static ICompilerReferenceError _CS0275;
        public static ICompilerReferenceError CS0276
        {
            get
            {
                if (_CS0276 == null)
                    _CS0276 = new CompilerReferenceError(Resources.CSharpErrors_CS0276, 276);
                return _CS0276;
            }
        }
        private static ICompilerReferenceError _CS0276;
        public static ICompilerReferenceError CS0277
        {
            get
            {
                if (_CS0277 == null)
                    _CS0277 = new CompilerReferenceError(Resources.CSharpErrors_CS0277, 277);
                return _CS0277;
            }
        }
        private static ICompilerReferenceError _CS0277;
        public static ICompilerReferenceError CS0281
        {
            get
            {
                if (_CS0281 == null)
                    _CS0281 = new CompilerReferenceError(Resources.CSharpErrors_CS0281, 281);
                return _CS0281;
            }
        }
        private static ICompilerReferenceError _CS0281;
        public static ICompilerReferenceError CS0283
        {
            get
            {
                if (_CS0283 == null)
                    _CS0283 = new CompilerReferenceError(Resources.CSharpErrors_CS0283, 283);
                return _CS0283;
            }
        }
        private static ICompilerReferenceError _CS0283;
        public static ICompilerReferenceError CS0304
        {
            get
            {
                if (_CS0304 == null)
                    _CS0304 = new CompilerReferenceError(Resources.CSharpErrors_CS0304, 304);
                return _CS0304;
            }
        }
        private static ICompilerReferenceError _CS0304;
        public static ICompilerReferenceError CS0305
        {
            get
            {
                if (_CS0305 == null)
                    _CS0305 = new CompilerReferenceError(Resources.CSharpErrors_CS0305, 305);
                return _CS0305;
            }
        }
        private static ICompilerReferenceError _CS0305;
        public static ICompilerReferenceError CS0306
        {
            get
            {
                if (_CS0306 == null)
                    _CS0306 = new CompilerReferenceError(Resources.CSharpErrors_CS0306, 306);
                return _CS0306;
            }
        }
        private static ICompilerReferenceError _CS0306;
        public static ICompilerReferenceError CS0307
        {
            get
            {
                if (_CS0307 == null)
                    _CS0307 = new CompilerReferenceError(Resources.CSharpErrors_CS0307, 307);
                return _CS0307;
            }
        }
        private static ICompilerReferenceError _CS0307;
        public static ICompilerReferenceError CS0308
        {
            get
            {
                if (_CS0308 == null)
                    _CS0308 = new CompilerReferenceError(Resources.CSharpErrors_CS0308, 308);
                return _CS0308;
            }
        }
        private static ICompilerReferenceError _CS0308;
        public static ICompilerReferenceError CS0310
        {
            get
            {
                if (_CS0310 == null)
                    _CS0310 = new CompilerReferenceError(Resources.CSharpErrors_CS0310, 310);
                return _CS0310;
            }
        }
        private static ICompilerReferenceError _CS0310;
        public static ICompilerReferenceError CS0311
        {
            get
            {
                if (_CS0311 == null)
                    _CS0311 = new CompilerReferenceError(Resources.CSharpErrors_CS0311, 311);
                return _CS0311;
            }
        }
        private static ICompilerReferenceError _CS0311;
        public static ICompilerReferenceError CS0312
        {
            get
            {
                if (_CS0312 == null)
                    _CS0312 = new CompilerReferenceError(Resources.CSharpErrors_CS0312, 312);
                return _CS0312;
            }
        }
        private static ICompilerReferenceError _CS0312;
        public static ICompilerReferenceError CS0313
        {
            get
            {
                if (_CS0313 == null)
                    _CS0313 = new CompilerReferenceError(Resources.CSharpErrors_CS0313, 313);
                return _CS0313;
            }
        }
        private static ICompilerReferenceError _CS0313;
        public static ICompilerReferenceError CS0314
        {
            get
            {
                if (_CS0314 == null)
                    _CS0314 = new CompilerReferenceError(Resources.CSharpErrors_CS0314, 314);
                return _CS0314;
            }
        }
        private static ICompilerReferenceError _CS0314;
        public static ICompilerReferenceError CS0315
        {
            get
            {
                if (_CS0315 == null)
                    _CS0315 = new CompilerReferenceError(Resources.CSharpErrors_CS0315, 315);
                return _CS0315;
            }
        }
        private static ICompilerReferenceError _CS0315;
        public static ICompilerReferenceError CS0316
        {
            get
            {
                if (_CS0316 == null)
                    _CS0316 = new CompilerReferenceError(Resources.CSharpErrors_CS0316, 316);
                return _CS0316;
            }
        }
        private static ICompilerReferenceError _CS0316;
        public static ICompilerReferenceError CS0400
        {
            get
            {
                if (_CS0400 == null)
                    _CS0400 = new CompilerReferenceError(Resources.CSharpErrors_CS0400, 400);
                return _CS0400;
            }
        }
        private static ICompilerReferenceError _CS0400;
        public static ICompilerReferenceError CS0401
        {
            get
            {
                if (_CS0401 == null)
                    _CS0401 = new CompilerReferenceError(Resources.CSharpErrors_CS0401, 401);
                return _CS0401;
            }
        }
        private static ICompilerReferenceError _CS0401;
        public static ICompilerReferenceError CS0403
        {
            get
            {
                if (_CS0403 == null)
                    _CS0403 = new CompilerReferenceError(Resources.CSharpErrors_CS0403, 403);
                return _CS0403;
            }
        }
        private static ICompilerReferenceError _CS0403;
        public static ICompilerReferenceError CS0404
        {
            get
            {
                if (_CS0404 == null)
                    _CS0404 = new CompilerReferenceError(Resources.CSharpErrors_CS0404, 404);
                return _CS0404;
            }
        }
        private static ICompilerReferenceError _CS0404;
        public static ICompilerReferenceError CS0405
        {
            get
            {
                if (_CS0405 == null)
                    _CS0405 = new CompilerReferenceError(Resources.CSharpErrors_CS0405, 405);
                return _CS0405;
            }
        }
        private static ICompilerReferenceError _CS0405;
        public static ICompilerReferenceError CS0406
        {
            get
            {
                if (_CS0406 == null)
                    _CS0406 = new CompilerReferenceError(Resources.CSharpErrors_CS0406, 406);
                return _CS0406;
            }
        }
        private static ICompilerReferenceError _CS0406;
        public static ICompilerReferenceError CS0407
        {
            get
            {
                if (_CS0407 == null)
                    _CS0407 = new CompilerReferenceError(Resources.CSharpErrors_CS0407, 407);
                return _CS0407;
            }
        }
        private static ICompilerReferenceError _CS0407;
        public static ICompilerReferenceError CS0409
        {
            get
            {
                if (_CS0409 == null)
                    _CS0409 = new CompilerReferenceError(Resources.CSharpErrors_CS0409, 409);
                return _CS0409;
            }
        }
        private static ICompilerReferenceError _CS0409;
        public static ICompilerReferenceError CS0410
        {
            get
            {
                if (_CS0410 == null)
                    _CS0410 = new CompilerReferenceError(Resources.CSharpErrors_CS0410, 410);
                return _CS0410;
            }
        }
        private static ICompilerReferenceError _CS0410;
        public static ICompilerReferenceError CS0411
        {
            get
            {
                if (_CS0411 == null)
                    _CS0411 = new CompilerReferenceError(Resources.CSharpErrors_CS0411, 411);
                return _CS0411;
            }
        }
        private static ICompilerReferenceError _CS0411;
        public static ICompilerReferenceError CS0412
        {
            get
            {
                if (_CS0412 == null)
                    _CS0412 = new CompilerReferenceError(Resources.CSharpErrors_CS0412, 412);
                return _CS0412;
            }
        }
        private static ICompilerReferenceError _CS0412;
        public static ICompilerReferenceError CS0413
        {
            get
            {
                if (_CS0413 == null)
                    _CS0413 = new CompilerReferenceError(Resources.CSharpErrors_CS0413, 413);
                return _CS0413;
            }
        }
        private static ICompilerReferenceError _CS0413;
        public static ICompilerReferenceError CS0415
        {
            get
            {
                if (_CS0415 == null)
                    _CS0415 = new CompilerReferenceError(Resources.CSharpErrors_CS0415, 415);
                return _CS0415;
            }
        }
        private static ICompilerReferenceError _CS0415;
        public static ICompilerReferenceError CS0416
        {
            get
            {
                if (_CS0416 == null)
                    _CS0416 = new CompilerReferenceError(Resources.CSharpErrors_CS0416, 416);
                return _CS0416;
            }
        }
        private static ICompilerReferenceError _CS0416;
        public static ICompilerReferenceError CS0417
        {
            get
            {
                if (_CS0417 == null)
                    _CS0417 = new CompilerReferenceError(Resources.CSharpErrors_CS0417, 417);
                return _CS0417;
            }
        }
        private static ICompilerReferenceError _CS0417;
        public static ICompilerReferenceError CS0418
        {
            get
            {
                if (_CS0418 == null)
                    _CS0418 = new CompilerReferenceError(Resources.CSharpErrors_CS0418, 418);
                return _CS0418;
            }
        }
        private static ICompilerReferenceError _CS0418;
        public static ICompilerReferenceError CS0423
        {
            get
            {
                if (_CS0423 == null)
                    _CS0423 = new CompilerReferenceError(Resources.CSharpErrors_CS0423, 423);
                return _CS0423;
            }
        }
        private static ICompilerReferenceError _CS0423;
        public static ICompilerReferenceError CS0424
        {
            get
            {
                if (_CS0424 == null)
                    _CS0424 = new CompilerReferenceError(Resources.CSharpErrors_CS0424, 424);
                return _CS0424;
            }
        }
        private static ICompilerReferenceError _CS0424;
        public static ICompilerReferenceError CS0425
        {
            get
            {
                if (_CS0425 == null)
                    _CS0425 = new CompilerReferenceError(Resources.CSharpErrors_CS0425, 425);
                return _CS0425;
            }
        }
        private static ICompilerReferenceError _CS0425;
        public static ICompilerReferenceError CS0426
        {
            get
            {
                if (_CS0426 == null)
                    _CS0426 = new CompilerReferenceError(Resources.CSharpErrors_CS0426, 426);
                return _CS0426;
            }
        }
        private static ICompilerReferenceError _CS0426;
        public static ICompilerReferenceError CS0428
        {
            get
            {
                if (_CS0428 == null)
                    _CS0428 = new CompilerReferenceError(Resources.CSharpErrors_CS0428, 428);
                return _CS0428;
            }
        }
        private static ICompilerReferenceError _CS0428;
        public static ICompilerReferenceError CS0430
        {
            get
            {
                if (_CS0430 == null)
                    _CS0430 = new CompilerReferenceError(Resources.CSharpErrors_CS0430, 430);
                return _CS0430;
            }
        }
        private static ICompilerReferenceError _CS0430;
        public static ICompilerReferenceError CS0431
        {
            get
            {
                if (_CS0431 == null)
                    _CS0431 = new CompilerReferenceError(Resources.CSharpErrors_CS0431, 431);
                return _CS0431;
            }
        }
        private static ICompilerReferenceError _CS0431;
        public static ICompilerReferenceError CS0432
        {
            get
            {
                if (_CS0432 == null)
                    _CS0432 = new CompilerReferenceError(Resources.CSharpErrors_CS0432, 432);
                return _CS0432;
            }
        }
        private static ICompilerReferenceError _CS0432;
        public static ICompilerReferenceError CS0433
        {
            get
            {
                if (_CS0433 == null)
                    _CS0433 = new CompilerReferenceError(Resources.CSharpErrors_CS0433, 433);
                return _CS0433;
            }
        }
        private static ICompilerReferenceError _CS0433;
        public static ICompilerReferenceError CS0434
        {
            get
            {
                if (_CS0434 == null)
                    _CS0434 = new CompilerReferenceError(Resources.CSharpErrors_CS0434, 434);
                return _CS0434;
            }
        }
        private static ICompilerReferenceError _CS0434;
        public static ICompilerReferenceError CS0438
        {
            get
            {
                if (_CS0438 == null)
                    _CS0438 = new CompilerReferenceError(Resources.CSharpErrors_CS0438, 438);
                return _CS0438;
            }
        }
        private static ICompilerReferenceError _CS0438;
        public static ICompilerReferenceError CS0439
        {
            get
            {
                if (_CS0439 == null)
                    _CS0439 = new CompilerReferenceError(Resources.CSharpErrors_CS0439, 439);
                return _CS0439;
            }
        }
        private static ICompilerReferenceError _CS0439;
        public static ICompilerReferenceError CS0441
        {
            get
            {
                if (_CS0441 == null)
                    _CS0441 = new CompilerReferenceError(Resources.CSharpErrors_CS0441, 441);
                return _CS0441;
            }
        }
        private static ICompilerReferenceError _CS0441;
        public static ICompilerReferenceError CS0442
        {
            get
            {
                if (_CS0442 == null)
                    _CS0442 = new CompilerReferenceError(Resources.CSharpErrors_CS0442, 442);
                return _CS0442;
            }
        }
        private static ICompilerReferenceError _CS0442;
        public static ICompilerReferenceError CS0443
        {
            get
            {
                if (_CS0443 == null)
                    _CS0443 = new CompilerReferenceError(Resources.CSharpErrors_CS0443, 443);
                return _CS0443;
            }
        }
        private static ICompilerReferenceError _CS0443;
        public static ICompilerReferenceError CS0445
        {
            get
            {
                if (_CS0445 == null)
                    _CS0445 = new CompilerReferenceError(Resources.CSharpErrors_CS0445, 445);
                return _CS0445;
            }
        }
        private static ICompilerReferenceError _CS0445;
        public static ICompilerReferenceError CS0446
        {
            get
            {
                if (_CS0446 == null)
                    _CS0446 = new CompilerReferenceError(Resources.CSharpErrors_CS0446, 446);
                return _CS0446;
            }
        }
        private static ICompilerReferenceError _CS0446;
        public static ICompilerReferenceError CS0447
        {
            get
            {
                if (_CS0447 == null)
                    _CS0447 = new CompilerReferenceError(Resources.CSharpErrors_CS0447, 447);
                return _CS0447;
            }
        }
        private static ICompilerReferenceError _CS0447;
        public static ICompilerReferenceError CS0448
        {
            get
            {
                if (_CS0448 == null)
                    _CS0448 = new CompilerReferenceError(Resources.CSharpErrors_CS0448, 448);
                return _CS0448;
            }
        }
        private static ICompilerReferenceError _CS0448;
        public static ICompilerReferenceError CS0449
        {
            get
            {
                if (_CS0449 == null)
                    _CS0449 = new CompilerReferenceError(Resources.CSharpErrors_CS0449, 449);
                return _CS0449;
            }
        }
        private static ICompilerReferenceError _CS0449;
        public static ICompilerReferenceError CS0450
        {
            get
            {
                if (_CS0450 == null)
                    _CS0450 = new CompilerReferenceError(Resources.CSharpErrors_CS0450, 450);
                return _CS0450;
            }
        }
        private static ICompilerReferenceError _CS0450;
        public static ICompilerReferenceError CS0451
        {
            get
            {
                if (_CS0451 == null)
                    _CS0451 = new CompilerReferenceError(Resources.CSharpErrors_CS0451, 451);
                return _CS0451;
            }
        }
        private static ICompilerReferenceError _CS0451;
        public static ICompilerReferenceError CS0452
        {
            get
            {
                if (_CS0452 == null)
                    _CS0452 = new CompilerReferenceError(Resources.CSharpErrors_CS0452, 452);
                return _CS0452;
            }
        }
        private static ICompilerReferenceError _CS0452;
        public static ICompilerReferenceError CS0453
        {
            get
            {
                if (_CS0453 == null)
                    _CS0453 = new CompilerReferenceError(Resources.CSharpErrors_CS0453, 453);
                return _CS0453;
            }
        }
        private static ICompilerReferenceError _CS0453;
        public static ICompilerReferenceError CS0454
        {
            get
            {
                if (_CS0454 == null)
                    _CS0454 = new CompilerReferenceError(Resources.CSharpErrors_CS0454, 454);
                return _CS0454;
            }
        }
        private static ICompilerReferenceError _CS0454;
        public static ICompilerReferenceError CS0455
        {
            get
            {
                if (_CS0455 == null)
                    _CS0455 = new CompilerReferenceError(Resources.CSharpErrors_CS0455, 455);
                return _CS0455;
            }
        }
        private static ICompilerReferenceError _CS0455;
        public static ICompilerReferenceError CS0456
        {
            get
            {
                if (_CS0456 == null)
                    _CS0456 = new CompilerReferenceError(Resources.CSharpErrors_CS0456, 456);
                return _CS0456;
            }
        }
        private static ICompilerReferenceError _CS0456;
        public static ICompilerReferenceError CS0457
        {
            get
            {
                if (_CS0457 == null)
                    _CS0457 = new CompilerReferenceError(Resources.CSharpErrors_CS0457, 457);
                return _CS0457;
            }
        }
        private static ICompilerReferenceError _CS0457;
        public static ICompilerReferenceError CS0459
        {
            get
            {
                if (_CS0459 == null)
                    _CS0459 = new CompilerReferenceError(Resources.CSharpErrors_CS0459, 459);
                return _CS0459;
            }
        }
        private static ICompilerReferenceError _CS0459;
        public static ICompilerReferenceError CS0460
        {
            get
            {
                if (_CS0460 == null)
                    _CS0460 = new CompilerReferenceError(Resources.CSharpErrors_CS0460, 460);
                return _CS0460;
            }
        }
        private static ICompilerReferenceError _CS0460;
        public static ICompilerReferenceError CS0462
        {
            get
            {
                if (_CS0462 == null)
                    _CS0462 = new CompilerReferenceError(Resources.CSharpErrors_CS0462, 462);
                return _CS0462;
            }
        }
        private static ICompilerReferenceError _CS0462;
        public static ICompilerReferenceError CS0463
        {
            get
            {
                if (_CS0463 == null)
                    _CS0463 = new CompilerReferenceError(Resources.CSharpErrors_CS0463, 463);
                return _CS0463;
            }
        }
        private static ICompilerReferenceError _CS0463;
        public static ICompilerReferenceError CS0466
        {
            get
            {
                if (_CS0466 == null)
                    _CS0466 = new CompilerReferenceError(Resources.CSharpErrors_CS0466, 466);
                return _CS0466;
            }
        }
        private static ICompilerReferenceError _CS0466;
        public static ICompilerReferenceError CS0468
        {
            get
            {
                if (_CS0468 == null)
                    _CS0468 = new CompilerReferenceError(Resources.CSharpErrors_CS0468, 468);
                return _CS0468;
            }
        }
        private static ICompilerReferenceError _CS0468;
        public static ICompilerReferenceError CS0470
        {
            get
            {
                if (_CS0470 == null)
                    _CS0470 = new CompilerReferenceError(Resources.CSharpErrors_CS0470, 470);
                return _CS0470;
            }
        }
        private static ICompilerReferenceError _CS0470;
        public static ICompilerReferenceError CS0471
        {
            get
            {
                if (_CS0471 == null)
                    _CS0471 = new CompilerReferenceError(Resources.CSharpErrors_CS0471, 471);
                return _CS0471;
            }
        }
        private static ICompilerReferenceError _CS0471;
        public static ICompilerReferenceError CS0473
        {
            get
            {
                if (_CS0473 == null)
                    _CS0473 = new CompilerReferenceError(Resources.CSharpErrors_CS0473, 473);
                return _CS0473;
            }
        }
        private static ICompilerReferenceError _CS0473;
        public static ICompilerReferenceError CS0500
        {
            get
            {
                if (_CS0500 == null)
                    _CS0500 = new CompilerReferenceError(Resources.CSharpErrors_CS0500, 500);
                return _CS0500;
            }
        }
        private static ICompilerReferenceError _CS0500;
        public static ICompilerReferenceError CS0501
        {
            get
            {
                if (_CS0501 == null)
                    _CS0501 = new CompilerReferenceError(Resources.CSharpErrors_CS0501, 501);
                return _CS0501;
            }
        }
        private static ICompilerReferenceError _CS0501;
        public static ICompilerReferenceError CS0502
        {
            get
            {
                if (_CS0502 == null)
                    _CS0502 = new CompilerReferenceError(Resources.CSharpErrors_CS0502, 502);
                return _CS0502;
            }
        }
        private static ICompilerReferenceError _CS0502;
        public static ICompilerReferenceError CS0503
        {
            get
            {
                if (_CS0503 == null)
                    _CS0503 = new CompilerReferenceError(Resources.CSharpErrors_CS0503, 503);
                return _CS0503;
            }
        }
        private static ICompilerReferenceError _CS0503;
        public static ICompilerReferenceError CS0504
        {
            get
            {
                if (_CS0504 == null)
                    _CS0504 = new CompilerReferenceError(Resources.CSharpErrors_CS0504, 504);
                return _CS0504;
            }
        }
        private static ICompilerReferenceError _CS0504;
        public static ICompilerReferenceError CS0505
        {
            get
            {
                if (_CS0505 == null)
                    _CS0505 = new CompilerReferenceError(Resources.CSharpErrors_CS0505, 505);
                return _CS0505;
            }
        }
        private static ICompilerReferenceError _CS0505;
        public static ICompilerReferenceError CS0506
        {
            get
            {
                if (_CS0506 == null)
                    _CS0506 = new CompilerReferenceError(Resources.CSharpErrors_CS0506, 506);
                return _CS0506;
            }
        }
        private static ICompilerReferenceError _CS0506;
        public static ICompilerReferenceError CS0507
        {
            get
            {
                if (_CS0507 == null)
                    _CS0507 = new CompilerReferenceError(Resources.CSharpErrors_CS0507, 507);
                return _CS0507;
            }
        }
        private static ICompilerReferenceError _CS0507;
        public static ICompilerReferenceError CS0508
        {
            get
            {
                if (_CS0508 == null)
                    _CS0508 = new CompilerReferenceError(Resources.CSharpErrors_CS0508, 508);
                return _CS0508;
            }
        }
        private static ICompilerReferenceError _CS0508;
        public static ICompilerReferenceError CS0509
        {
            get
            {
                if (_CS0509 == null)
                    _CS0509 = new CompilerReferenceError(Resources.CSharpErrors_CS0509, 509);
                return _CS0509;
            }
        }
        private static ICompilerReferenceError _CS0509;
        public static ICompilerReferenceError CS0513
        {
            get
            {
                if (_CS0513 == null)
                    _CS0513 = new CompilerReferenceError(Resources.CSharpErrors_CS0513, 513);
                return _CS0513;
            }
        }
        private static ICompilerReferenceError _CS0513;
        public static ICompilerReferenceError CS0514
        {
            get
            {
                if (_CS0514 == null)
                    _CS0514 = new CompilerReferenceError(Resources.CSharpErrors_CS0514, 514);
                return _CS0514;
            }
        }
        private static ICompilerReferenceError _CS0514;
        public static ICompilerReferenceError CS0515
        {
            get
            {
                if (_CS0515 == null)
                    _CS0515 = new CompilerReferenceError(Resources.CSharpErrors_CS0515, 515);
                return _CS0515;
            }
        }
        private static ICompilerReferenceError _CS0515;
        public static ICompilerReferenceError CS0516
        {
            get
            {
                if (_CS0516 == null)
                    _CS0516 = new CompilerReferenceError(Resources.CSharpErrors_CS0516, 516);
                return _CS0516;
            }
        }
        private static ICompilerReferenceError _CS0516;
        public static ICompilerReferenceError CS0517
        {
            get
            {
                if (_CS0517 == null)
                    _CS0517 = new CompilerReferenceError(Resources.CSharpErrors_CS0517, 517);
                return _CS0517;
            }
        }
        private static ICompilerReferenceError _CS0517;
        public static ICompilerReferenceError CS0518
        {
            get
            {
                if (_CS0518 == null)
                    _CS0518 = new CompilerReferenceError(Resources.CSharpErrors_CS0518, 518);
                return _CS0518;
            }
        }
        private static ICompilerReferenceError _CS0518;
        public static ICompilerReferenceError CS0520
        {
            get
            {
                if (_CS0520 == null)
                    _CS0520 = new CompilerReferenceError(Resources.CSharpErrors_CS0520, 520);
                return _CS0520;
            }
        }
        private static ICompilerReferenceError _CS0520;
        public static ICompilerReferenceError CS0522
        {
            get
            {
                if (_CS0522 == null)
                    _CS0522 = new CompilerReferenceError(Resources.CSharpErrors_CS0522, 522);
                return _CS0522;
            }
        }
        private static ICompilerReferenceError _CS0522;
        public static ICompilerReferenceError CS0523
        {
            get
            {
                if (_CS0523 == null)
                    _CS0523 = new CompilerReferenceError(Resources.CSharpErrors_CS0523, 523);
                return _CS0523;
            }
        }
        private static ICompilerReferenceError _CS0523;
        public static ICompilerReferenceError CS0524
        {
            get
            {
                if (_CS0524 == null)
                    _CS0524 = new CompilerReferenceError(Resources.CSharpErrors_CS0524, 524);
                return _CS0524;
            }
        }
        private static ICompilerReferenceError _CS0524;
        public static ICompilerReferenceError CS0525
        {
            get
            {
                if (_CS0525 == null)
                    _CS0525 = new CompilerReferenceError(Resources.CSharpErrors_CS0525, 525);
                return _CS0525;
            }
        }
        private static ICompilerReferenceError _CS0525;
        public static ICompilerReferenceError CS0526
        {
            get
            {
                if (_CS0526 == null)
                    _CS0526 = new CompilerReferenceError(Resources.CSharpErrors_CS0526, 526);
                return _CS0526;
            }
        }
        private static ICompilerReferenceError _CS0526;
        public static ICompilerReferenceError CS0527
        {
            get
            {
                if (_CS0527 == null)
                    _CS0527 = new CompilerReferenceError(Resources.CSharpErrors_CS0527, 527);
                return _CS0527;
            }
        }
        private static ICompilerReferenceError _CS0527;
        public static ICompilerReferenceError CS0528
        {
            get
            {
                if (_CS0528 == null)
                    _CS0528 = new CompilerReferenceError(Resources.CSharpErrors_CS0528, 528);
                return _CS0528;
            }
        }
        private static ICompilerReferenceError _CS0528;
        public static ICompilerReferenceError CS0529
        {
            get
            {
                if (_CS0529 == null)
                    _CS0529 = new CompilerReferenceError(Resources.CSharpErrors_CS0529, 529);
                return _CS0529;
            }
        }
        private static ICompilerReferenceError _CS0529;
        public static ICompilerReferenceError CS0531
        {
            get
            {
                if (_CS0531 == null)
                    _CS0531 = new CompilerReferenceError(Resources.CSharpErrors_CS0531, 531);
                return _CS0531;
            }
        }
        private static ICompilerReferenceError _CS0531;
        public static ICompilerReferenceError CS0533
        {
            get
            {
                if (_CS0533 == null)
                    _CS0533 = new CompilerReferenceError(Resources.CSharpErrors_CS0533, 533);
                return _CS0533;
            }
        }
        private static ICompilerReferenceError _CS0533;
        public static ICompilerReferenceError CS0534
        {
            get
            {
                if (_CS0534 == null)
                    _CS0534 = new CompilerReferenceError(Resources.CSharpErrors_CS0534, 534);
                return _CS0534;
            }
        }
        private static ICompilerReferenceError _CS0534;
        public static ICompilerReferenceError CS0535
        {
            get
            {
                if (_CS0535 == null)
                    _CS0535 = new CompilerReferenceError(Resources.CSharpErrors_CS0535, 535);
                return _CS0535;
            }
        }
        private static ICompilerReferenceError _CS0535;
        public static ICompilerReferenceError CS0537
        {
            get
            {
                if (_CS0537 == null)
                    _CS0537 = new CompilerReferenceError(Resources.CSharpErrors_CS0537, 537);
                return _CS0537;
            }
        }
        private static ICompilerReferenceError _CS0537;
        public static ICompilerReferenceError CS0538
        {
            get
            {
                if (_CS0538 == null)
                    _CS0538 = new CompilerReferenceError(Resources.CSharpErrors_CS0538, 538);
                return _CS0538;
            }
        }
        private static ICompilerReferenceError _CS0538;
        public static ICompilerReferenceError CS0539
        {
            get
            {
                if (_CS0539 == null)
                    _CS0539 = new CompilerReferenceError(Resources.CSharpErrors_CS0539, 539);
                return _CS0539;
            }
        }
        private static ICompilerReferenceError _CS0539;
        public static ICompilerReferenceError CS0540
        {
            get
            {
                if (_CS0540 == null)
                    _CS0540 = new CompilerReferenceError(Resources.CSharpErrors_CS0540, 540);
                return _CS0540;
            }
        }
        private static ICompilerReferenceError _CS0540;
        public static ICompilerReferenceError CS0541
        {
            get
            {
                if (_CS0541 == null)
                    _CS0541 = new CompilerReferenceError(Resources.CSharpErrors_CS0541, 541);
                return _CS0541;
            }
        }
        private static ICompilerReferenceError _CS0541;
        public static ICompilerReferenceError CS0542
        {
            get
            {
                if (_CS0542 == null)
                    _CS0542 = new CompilerReferenceError(Resources.CSharpErrors_CS0542, 542);
                return _CS0542;
            }
        }
        private static ICompilerReferenceError _CS0542;
        public static ICompilerReferenceError CS0543
        {
            get
            {
                if (_CS0543 == null)
                    _CS0543 = new CompilerReferenceError(Resources.CSharpErrors_CS0543, 543);
                return _CS0543;
            }
        }
        private static ICompilerReferenceError _CS0543;
        public static ICompilerReferenceError CS0544
        {
            get
            {
                if (_CS0544 == null)
                    _CS0544 = new CompilerReferenceError(Resources.CSharpErrors_CS0544, 544);
                return _CS0544;
            }
        }
        private static ICompilerReferenceError _CS0544;
        public static ICompilerReferenceError CS0545
        {
            get
            {
                if (_CS0545 == null)
                    _CS0545 = new CompilerReferenceError(Resources.CSharpErrors_CS0545, 545);
                return _CS0545;
            }
        }
        private static ICompilerReferenceError _CS0545;
        public static ICompilerReferenceError CS0546
        {
            get
            {
                if (_CS0546 == null)
                    _CS0546 = new CompilerReferenceError(Resources.CSharpErrors_CS0546, 546);
                return _CS0546;
            }
        }
        private static ICompilerReferenceError _CS0546;
        public static ICompilerReferenceError CS0547
        {
            get
            {
                if (_CS0547 == null)
                    _CS0547 = new CompilerReferenceError(Resources.CSharpErrors_CS0547, 547);
                return _CS0547;
            }
        }
        private static ICompilerReferenceError _CS0547;
        public static ICompilerReferenceError CS0548
        {
            get
            {
                if (_CS0548 == null)
                    _CS0548 = new CompilerReferenceError(Resources.CSharpErrors_CS0548, 548);
                return _CS0548;
            }
        }
        private static ICompilerReferenceError _CS0548;
        public static ICompilerReferenceError CS0549
        {
            get
            {
                if (_CS0549 == null)
                    _CS0549 = new CompilerReferenceError(Resources.CSharpErrors_CS0549, 549);
                return _CS0549;
            }
        }
        private static ICompilerReferenceError _CS0549;
        public static ICompilerReferenceError CS0550
        {
            get
            {
                if (_CS0550 == null)
                    _CS0550 = new CompilerReferenceError(Resources.CSharpErrors_CS0550, 550);
                return _CS0550;
            }
        }
        private static ICompilerReferenceError _CS0550;
        public static ICompilerReferenceError CS0551
        {
            get
            {
                if (_CS0551 == null)
                    _CS0551 = new CompilerReferenceError(Resources.CSharpErrors_CS0551, 551);
                return _CS0551;
            }
        }
        private static ICompilerReferenceError _CS0551;
        public static ICompilerReferenceError CS0552
        {
            get
            {
                if (_CS0552 == null)
                    _CS0552 = new CompilerReferenceError(Resources.CSharpErrors_CS0552, 552);
                return _CS0552;
            }
        }
        private static ICompilerReferenceError _CS0552;
        public static ICompilerReferenceError CS0553
        {
            get
            {
                if (_CS0553 == null)
                    _CS0553 = new CompilerReferenceError(Resources.CSharpErrors_CS0553, 553);
                return _CS0553;
            }
        }
        private static ICompilerReferenceError _CS0553;
        public static ICompilerReferenceError CS0554
        {
            get
            {
                if (_CS0554 == null)
                    _CS0554 = new CompilerReferenceError(Resources.CSharpErrors_CS0554, 554);
                return _CS0554;
            }
        }
        private static ICompilerReferenceError _CS0554;
        public static ICompilerReferenceError CS0555
        {
            get
            {
                if (_CS0555 == null)
                    _CS0555 = new CompilerReferenceError(Resources.CSharpErrors_CS0555, 555);
                return _CS0555;
            }
        }
        private static ICompilerReferenceError _CS0555;
        public static ICompilerReferenceError CS0556
        {
            get
            {
                if (_CS0556 == null)
                    _CS0556 = new CompilerReferenceError(Resources.CSharpErrors_CS0556, 556);
                return _CS0556;
            }
        }
        private static ICompilerReferenceError _CS0556;
        public static ICompilerReferenceError CS0557
        {
            get
            {
                if (_CS0557 == null)
                    _CS0557 = new CompilerReferenceError(Resources.CSharpErrors_CS0557, 557);
                return _CS0557;
            }
        }
        private static ICompilerReferenceError _CS0557;
        public static ICompilerReferenceError CS0558
        {
            get
            {
                if (_CS0558 == null)
                    _CS0558 = new CompilerReferenceError(Resources.CSharpErrors_CS0558, 558);
                return _CS0558;
            }
        }
        private static ICompilerReferenceError _CS0558;
        public static ICompilerReferenceError CS0559
        {
            get
            {
                if (_CS0559 == null)
                    _CS0559 = new CompilerReferenceError(Resources.CSharpErrors_CS0559, 559);
                return _CS0559;
            }
        }
        private static ICompilerReferenceError _CS0559;
        public static ICompilerReferenceError CS0562
        {
            get
            {
                if (_CS0562 == null)
                    _CS0562 = new CompilerReferenceError(Resources.CSharpErrors_CS0562, 562);
                return _CS0562;
            }
        }
        private static ICompilerReferenceError _CS0562;
        public static ICompilerReferenceError CS0563
        {
            get
            {
                if (_CS0563 == null)
                    _CS0563 = new CompilerReferenceError(Resources.CSharpErrors_CS0563, 563);
                return _CS0563;
            }
        }
        private static ICompilerReferenceError _CS0563;
        public static ICompilerReferenceError CS0564
        {
            get
            {
                if (_CS0564 == null)
                    _CS0564 = new CompilerReferenceError(Resources.CSharpErrors_CS0564, 564);
                return _CS0564;
            }
        }
        private static ICompilerReferenceError _CS0564;
        public static ICompilerReferenceError CS0567
        {
            get
            {
                if (_CS0567 == null)
                    _CS0567 = new CompilerReferenceError(Resources.CSharpErrors_CS0567, 567);
                return _CS0567;
            }
        }
        private static ICompilerReferenceError _CS0567;
        public static ICompilerReferenceError CS0568
        {
            get
            {
                if (_CS0568 == null)
                    _CS0568 = new CompilerReferenceError(Resources.CSharpErrors_CS0568, 568);
                return _CS0568;
            }
        }
        private static ICompilerReferenceError _CS0568;
        public static ICompilerReferenceError CS0569
        {
            get
            {
                if (_CS0569 == null)
                    _CS0569 = new CompilerReferenceError(Resources.CSharpErrors_CS0569, 569);
                return _CS0569;
            }
        }
        private static ICompilerReferenceError _CS0569;
        public static ICompilerReferenceError CS0570
        {
            get
            {
                if (_CS0570 == null)
                    _CS0570 = new CompilerReferenceError(Resources.CSharpErrors_CS0570, 570);
                return _CS0570;
            }
        }
        private static ICompilerReferenceError _CS0570;
        public static ICompilerReferenceError CS0571
        {
            get
            {
                if (_CS0571 == null)
                    _CS0571 = new CompilerReferenceError(Resources.CSharpErrors_CS0571, 571);
                return _CS0571;
            }
        }
        private static ICompilerReferenceError _CS0571;
        public static ICompilerReferenceError CS0572
        {
            get
            {
                if (_CS0572 == null)
                    _CS0572 = new CompilerReferenceError(Resources.CSharpErrors_CS0572, 572);
                return _CS0572;
            }
        }
        private static ICompilerReferenceError _CS0572;
        public static ICompilerReferenceError CS0573
        {
            get
            {
                if (_CS0573 == null)
                    _CS0573 = new CompilerReferenceError(Resources.CSharpErrors_CS0573, 573);
                return _CS0573;
            }
        }
        private static ICompilerReferenceError _CS0573;
        public static ICompilerReferenceError CS0574
        {
            get
            {
                if (_CS0574 == null)
                    _CS0574 = new CompilerReferenceError(Resources.CSharpErrors_CS0574, 574);
                return _CS0574;
            }
        }
        private static ICompilerReferenceError _CS0574;
        public static ICompilerReferenceError CS0575
        {
            get
            {
                if (_CS0575 == null)
                    _CS0575 = new CompilerReferenceError(Resources.CSharpErrors_CS0575, 575);
                return _CS0575;
            }
        }
        private static ICompilerReferenceError _CS0575;
        public static ICompilerReferenceError CS0576
        {
            get
            {
                if (_CS0576 == null)
                    _CS0576 = new CompilerReferenceError(Resources.CSharpErrors_CS0576, 576);
                return _CS0576;
            }
        }
        private static ICompilerReferenceError _CS0576;
        public static ICompilerReferenceError CS0577
        {
            get
            {
                if (_CS0577 == null)
                    _CS0577 = new CompilerReferenceError(Resources.CSharpErrors_CS0577, 577);
                return _CS0577;
            }
        }
        private static ICompilerReferenceError _CS0577;
        public static ICompilerReferenceError CS0578
        {
            get
            {
                if (_CS0578 == null)
                    _CS0578 = new CompilerReferenceError(Resources.CSharpErrors_CS0578, 578);
                return _CS0578;
            }
        }
        private static ICompilerReferenceError _CS0578;
        public static ICompilerReferenceError CS0579
        {
            get
            {
                if (_CS0579 == null)
                    _CS0579 = new CompilerReferenceError(Resources.CSharpErrors_CS0579, 579);
                return _CS0579;
            }
        }
        private static ICompilerReferenceError _CS0579;
        public static ICompilerReferenceError CS0582
        {
            get
            {
                if (_CS0582 == null)
                    _CS0582 = new CompilerReferenceError(Resources.CSharpErrors_CS0582, 582);
                return _CS0582;
            }
        }
        private static ICompilerReferenceError _CS0582;
        public static ICompilerReferenceError CS0583
        {
            get
            {
                if (_CS0583 == null)
                    _CS0583 = new CompilerReferenceError(Resources.CSharpErrors_CS0583, 583);
                return _CS0583;
            }
        }
        private static ICompilerReferenceError _CS0583;
        public static ICompilerReferenceError CS0584
        {
            get
            {
                if (_CS0584 == null)
                    _CS0584 = new CompilerReferenceError(Resources.CSharpErrors_CS0584, 584);
                return _CS0584;
            }
        }
        private static ICompilerReferenceError _CS0584;
        public static ICompilerReferenceError CS0585
        {
            get
            {
                if (_CS0585 == null)
                    _CS0585 = new CompilerReferenceError(Resources.CSharpErrors_CS0585, 585);
                return _CS0585;
            }
        }
        private static ICompilerReferenceError _CS0585;
        public static ICompilerReferenceError CS0586
        {
            get
            {
                if (_CS0586 == null)
                    _CS0586 = new CompilerReferenceError(Resources.CSharpErrors_CS0586, 586);
                return _CS0586;
            }
        }
        private static ICompilerReferenceError _CS0586;
        public static ICompilerReferenceError CS0587
        {
            get
            {
                if (_CS0587 == null)
                    _CS0587 = new CompilerReferenceError(Resources.CSharpErrors_CS0587, 587);
                return _CS0587;
            }
        }
        private static ICompilerReferenceError _CS0587;
        public static ICompilerReferenceError CS0588
        {
            get
            {
                if (_CS0588 == null)
                    _CS0588 = new CompilerReferenceError(Resources.CSharpErrors_CS0588, 588);
                return _CS0588;
            }
        }
        private static ICompilerReferenceError _CS0588;
        public static ICompilerReferenceError CS0589
        {
            get
            {
                if (_CS0589 == null)
                    _CS0589 = new CompilerReferenceError(Resources.CSharpErrors_CS0589, 589);
                return _CS0589;
            }
        }
        private static ICompilerReferenceError _CS0589;
        public static ICompilerReferenceError CS0590
        {
            get
            {
                if (_CS0590 == null)
                    _CS0590 = new CompilerReferenceError(Resources.CSharpErrors_CS0590, 590);
                return _CS0590;
            }
        }
        private static ICompilerReferenceError _CS0590;
        public static ICompilerReferenceError CS0591
        {
            get
            {
                if (_CS0591 == null)
                    _CS0591 = new CompilerReferenceError(Resources.CSharpErrors_CS0591, 591);
                return _CS0591;
            }
        }
        private static ICompilerReferenceError _CS0591;
        public static ICompilerReferenceError CS0592
        {
            get
            {
                if (_CS0592 == null)
                    _CS0592 = new CompilerReferenceError(Resources.CSharpErrors_CS0592, 592);
                return _CS0592;
            }
        }
        private static ICompilerReferenceError _CS0592;
        public static ICompilerReferenceError CS0594
        {
            get
            {
                if (_CS0594 == null)
                    _CS0594 = new CompilerReferenceError(Resources.CSharpErrors_CS0594, 594);
                return _CS0594;
            }
        }
        private static ICompilerReferenceError _CS0594;
        public static ICompilerReferenceError CS0596
        {
            get
            {
                if (_CS0596 == null)
                    _CS0596 = new CompilerReferenceError(Resources.CSharpErrors_CS0596, 596);
                return _CS0596;
            }
        }
        private static ICompilerReferenceError _CS0596;
        public static ICompilerReferenceError CS0599
        {
            get
            {
                if (_CS0599 == null)
                    _CS0599 = new CompilerReferenceError(Resources.CSharpErrors_CS0599, 599);
                return _CS0599;
            }
        }
        private static ICompilerReferenceError _CS0599;
        public static ICompilerReferenceError CS0601
        {
            get
            {
                if (_CS0601 == null)
                    _CS0601 = new CompilerReferenceError(Resources.CSharpErrors_CS0601, 601);
                return _CS0601;
            }
        }
        private static ICompilerReferenceError _CS0601;
        public static ICompilerReferenceError CS0609
        {
            get
            {
                if (_CS0609 == null)
                    _CS0609 = new CompilerReferenceError(Resources.CSharpErrors_CS0609, 609);
                return _CS0609;
            }
        }
        private static ICompilerReferenceError _CS0609;
        public static ICompilerReferenceError CS0610
        {
            get
            {
                if (_CS0610 == null)
                    _CS0610 = new CompilerReferenceError(Resources.CSharpErrors_CS0610, 610);
                return _CS0610;
            }
        }
        private static ICompilerReferenceError _CS0610;
        public static ICompilerReferenceError CS0611
        {
            get
            {
                if (_CS0611 == null)
                    _CS0611 = new CompilerReferenceError(Resources.CSharpErrors_CS0611, 611);
                return _CS0611;
            }
        }
        private static ICompilerReferenceError _CS0611;
        public static ICompilerReferenceError CS0616
        {
            get
            {
                if (_CS0616 == null)
                    _CS0616 = new CompilerReferenceError(Resources.CSharpErrors_CS0616, 616);
                return _CS0616;
            }
        }
        private static ICompilerReferenceError _CS0616;
        public static ICompilerReferenceError CS0617
        {
            get
            {
                if (_CS0617 == null)
                    _CS0617 = new CompilerReferenceError(Resources.CSharpErrors_CS0617, 617);
                return _CS0617;
            }
        }
        private static ICompilerReferenceError _CS0617;
        public static ICompilerReferenceError CS0619
        {
            get
            {
                if (_CS0619 == null)
                    _CS0619 = new CompilerReferenceError(Resources.CSharpErrors_CS0619, 619);
                return _CS0619;
            }
        }
        private static ICompilerReferenceError _CS0619;
        public static ICompilerReferenceError CS0620
        {
            get
            {
                if (_CS0620 == null)
                    _CS0620 = new CompilerReferenceError(Resources.CSharpErrors_CS0620, 620);
                return _CS0620;
            }
        }
        private static ICompilerReferenceError _CS0620;
        public static ICompilerReferenceError CS0621
        {
            get
            {
                if (_CS0621 == null)
                    _CS0621 = new CompilerReferenceError(Resources.CSharpErrors_CS0621, 621);
                return _CS0621;
            }
        }
        private static ICompilerReferenceError _CS0621;
        public static ICompilerReferenceError CS0622
        {
            get
            {
                if (_CS0622 == null)
                    _CS0622 = new CompilerReferenceError(Resources.CSharpErrors_CS0622, 622);
                return _CS0622;
            }
        }
        private static ICompilerReferenceError _CS0622;
        public static ICompilerReferenceError CS0623
        {
            get
            {
                if (_CS0623 == null)
                    _CS0623 = new CompilerReferenceError(Resources.CSharpErrors_CS0623, 623);
                return _CS0623;
            }
        }
        private static ICompilerReferenceError _CS0623;
        public static ICompilerReferenceError CS0625
        {
            get
            {
                if (_CS0625 == null)
                    _CS0625 = new CompilerReferenceError(Resources.CSharpErrors_CS0625, 625);
                return _CS0625;
            }
        }
        private static ICompilerReferenceError _CS0625;
        public static ICompilerReferenceError CS0629
        {
            get
            {
                if (_CS0629 == null)
                    _CS0629 = new CompilerReferenceError(Resources.CSharpErrors_CS0629, 629);
                return _CS0629;
            }
        }
        private static ICompilerReferenceError _CS0629;
        public static ICompilerReferenceError CS0631
        {
            get
            {
                if (_CS0631 == null)
                    _CS0631 = new CompilerReferenceError(Resources.CSharpErrors_CS0631, 631);
                return _CS0631;
            }
        }
        private static ICompilerReferenceError _CS0631;
        public static ICompilerReferenceError CS0633
        {
            get
            {
                if (_CS0633 == null)
                    _CS0633 = new CompilerReferenceError(Resources.CSharpErrors_CS0633, 633);
                return _CS0633;
            }
        }
        private static ICompilerReferenceError _CS0633;
        public static ICompilerReferenceError CS0635
        {
            get
            {
                if (_CS0635 == null)
                    _CS0635 = new CompilerReferenceError(Resources.CSharpErrors_CS0635, 635);
                return _CS0635;
            }
        }
        private static ICompilerReferenceError _CS0635;
        public static ICompilerReferenceError CS0636
        {
            get
            {
                if (_CS0636 == null)
                    _CS0636 = new CompilerReferenceError(Resources.CSharpErrors_CS0636, 636);
                return _CS0636;
            }
        }
        private static ICompilerReferenceError _CS0636;
        public static ICompilerReferenceError CS0637
        {
            get
            {
                if (_CS0637 == null)
                    _CS0637 = new CompilerReferenceError(Resources.CSharpErrors_CS0637, 637);
                return _CS0637;
            }
        }
        private static ICompilerReferenceError _CS0637;
        public static ICompilerReferenceError CS0641
        {
            get
            {
                if (_CS0641 == null)
                    _CS0641 = new CompilerReferenceError(Resources.CSharpErrors_CS0641, 641);
                return _CS0641;
            }
        }
        private static ICompilerReferenceError _CS0641;
        public static ICompilerReferenceError CS0643
        {
            get
            {
                if (_CS0643 == null)
                    _CS0643 = new CompilerReferenceError(Resources.CSharpErrors_CS0643, 643);
                return _CS0643;
            }
        }
        private static ICompilerReferenceError _CS0643;
        public static ICompilerReferenceError CS0644
        {
            get
            {
                if (_CS0644 == null)
                    _CS0644 = new CompilerReferenceError(Resources.CSharpErrors_CS0644, 644);
                return _CS0644;
            }
        }
        private static ICompilerReferenceError _CS0644;
        public static ICompilerReferenceError CS0645
        {
            get
            {
                if (_CS0645 == null)
                    _CS0645 = new CompilerReferenceError(Resources.CSharpErrors_CS0645, 645);
                return _CS0645;
            }
        }
        private static ICompilerReferenceError _CS0645;
        public static ICompilerReferenceError CS0646
        {
            get
            {
                if (_CS0646 == null)
                    _CS0646 = new CompilerReferenceError(Resources.CSharpErrors_CS0646, 646);
                return _CS0646;
            }
        }
        private static ICompilerReferenceError _CS0646;
        public static ICompilerReferenceError CS0647
        {
            get
            {
                if (_CS0647 == null)
                    _CS0647 = new CompilerReferenceError(Resources.CSharpErrors_CS0647, 647);
                return _CS0647;
            }
        }
        private static ICompilerReferenceError _CS0647;
        public static ICompilerReferenceError CS0648
        {
            get
            {
                if (_CS0648 == null)
                    _CS0648 = new CompilerReferenceError(Resources.CSharpErrors_CS0648, 648);
                return _CS0648;
            }
        }
        private static ICompilerReferenceError _CS0648;
        public static ICompilerReferenceError CS0650
        {
            get
            {
                if (_CS0650 == null)
                    _CS0650 = new CompilerReferenceError(Resources.CSharpErrors_CS0650, 650);
                return _CS0650;
            }
        }
        private static ICompilerReferenceError _CS0650;
        public static ICompilerReferenceError CS0653
        {
            get
            {
                if (_CS0653 == null)
                    _CS0653 = new CompilerReferenceError(Resources.CSharpErrors_CS0653, 653);
                return _CS0653;
            }
        }
        private static ICompilerReferenceError _CS0653;
        public static ICompilerReferenceError CS0655
        {
            get
            {
                if (_CS0655 == null)
                    _CS0655 = new CompilerReferenceError(Resources.CSharpErrors_CS0655, 655);
                return _CS0655;
            }
        }
        private static ICompilerReferenceError _CS0655;
        public static ICompilerReferenceError CS0656
        {
            get
            {
                if (_CS0656 == null)
                    _CS0656 = new CompilerReferenceError(Resources.CSharpErrors_CS0656, 656);
                return _CS0656;
            }
        }
        private static ICompilerReferenceError _CS0656;
        public static ICompilerReferenceError CS0662
        {
            get
            {
                if (_CS0662 == null)
                    _CS0662 = new CompilerReferenceError(Resources.CSharpErrors_CS0662, 662);
                return _CS0662;
            }
        }
        private static ICompilerReferenceError _CS0662;
        public static ICompilerReferenceError CS0663
        {
            get
            {
                if (_CS0663 == null)
                    _CS0663 = new CompilerReferenceError(Resources.CSharpErrors_CS0663, 663);
                return _CS0663;
            }
        }
        private static ICompilerReferenceError _CS0663;
        public static ICompilerReferenceError CS0664
        {
            get
            {
                if (_CS0664 == null)
                    _CS0664 = new CompilerReferenceError(Resources.CSharpErrors_CS0664, 664);
                return _CS0664;
            }
        }
        private static ICompilerReferenceError _CS0664;
        public static ICompilerReferenceError CS0666
        {
            get
            {
                if (_CS0666 == null)
                    _CS0666 = new CompilerReferenceError(Resources.CSharpErrors_CS0666, 666);
                return _CS0666;
            }
        }
        private static ICompilerReferenceError _CS0666;
        public static ICompilerReferenceError CS0667
        {
            get
            {
                if (_CS0667 == null)
                    _CS0667 = new CompilerReferenceError(Resources.CSharpErrors_CS0667, 667);
                return _CS0667;
            }
        }
        private static ICompilerReferenceError _CS0667;
        public static ICompilerReferenceError CS0668
        {
            get
            {
                if (_CS0668 == null)
                    _CS0668 = new CompilerReferenceError(Resources.CSharpErrors_CS0668, 668);
                return _CS0668;
            }
        }
        private static ICompilerReferenceError _CS0668;
        public static ICompilerReferenceError CS0669
        {
            get
            {
                if (_CS0669 == null)
                    _CS0669 = new CompilerReferenceError(Resources.CSharpErrors_CS0669, 669);
                return _CS0669;
            }
        }
        private static ICompilerReferenceError _CS0669;
        public static ICompilerReferenceError CS0670
        {
            get
            {
                if (_CS0670 == null)
                    _CS0670 = new CompilerReferenceError(Resources.CSharpErrors_CS0670, 670);
                return _CS0670;
            }
        }
        private static ICompilerReferenceError _CS0670;
        public static ICompilerReferenceError CS0673
        {
            get
            {
                if (_CS0673 == null)
                    _CS0673 = new CompilerReferenceError(Resources.CSharpErrors_CS0673, 673);
                return _CS0673;
            }
        }
        private static ICompilerReferenceError _CS0673;
        public static ICompilerReferenceError CS0674
        {
            get
            {
                if (_CS0674 == null)
                    _CS0674 = new CompilerReferenceError(Resources.CSharpErrors_CS0674, 674);
                return _CS0674;
            }
        }
        private static ICompilerReferenceError _CS0674;
        public static ICompilerReferenceError CS0677
        {
            get
            {
                if (_CS0677 == null)
                    _CS0677 = new CompilerReferenceError(Resources.CSharpErrors_CS0677, 677);
                return _CS0677;
            }
        }
        private static ICompilerReferenceError _CS0677;
        public static ICompilerReferenceError CS0678
        {
            get
            {
                if (_CS0678 == null)
                    _CS0678 = new CompilerReferenceError(Resources.CSharpErrors_CS0678, 678);
                return _CS0678;
            }
        }
        private static ICompilerReferenceError _CS0678;
        public static ICompilerReferenceError CS0681
        {
            get
            {
                if (_CS0681 == null)
                    _CS0681 = new CompilerReferenceError(Resources.CSharpErrors_CS0681, 681);
                return _CS0681;
            }
        }
        private static ICompilerReferenceError _CS0681;
        public static ICompilerReferenceError CS0682
        {
            get
            {
                if (_CS0682 == null)
                    _CS0682 = new CompilerReferenceError(Resources.CSharpErrors_CS0682, 682);
                return _CS0682;
            }
        }
        private static ICompilerReferenceError _CS0682;
        public static ICompilerReferenceError CS0683
        {
            get
            {
                if (_CS0683 == null)
                    _CS0683 = new CompilerReferenceError(Resources.CSharpErrors_CS0683, 683);
                return _CS0683;
            }
        }
        private static ICompilerReferenceError _CS0683;
        public static ICompilerReferenceError CS0685
        {
            get
            {
                if (_CS0685 == null)
                    _CS0685 = new CompilerReferenceError(Resources.CSharpErrors_CS0685, 685);
                return _CS0685;
            }
        }
        private static ICompilerReferenceError _CS0685;
        public static ICompilerReferenceError CS0686
        {
            get
            {
                if (_CS0686 == null)
                    _CS0686 = new CompilerReferenceError(Resources.CSharpErrors_CS0686, 686);
                return _CS0686;
            }
        }
        private static ICompilerReferenceError _CS0686;
        public static ICompilerReferenceError CS0687
        {
            get
            {
                if (_CS0687 == null)
                    _CS0687 = new CompilerReferenceError(Resources.CSharpErrors_CS0687, 687);
                return _CS0687;
            }
        }
        private static ICompilerReferenceError _CS0687;
        public static ICompilerReferenceError CS0689
        {
            get
            {
                if (_CS0689 == null)
                    _CS0689 = new CompilerReferenceError(Resources.CSharpErrors_CS0689, 689);
                return _CS0689;
            }
        }
        private static ICompilerReferenceError _CS0689;
        public static ICompilerReferenceError CS0690
        {
            get
            {
                if (_CS0690 == null)
                    _CS0690 = new CompilerReferenceError(Resources.CSharpErrors_CS0690, 690);
                return _CS0690;
            }
        }
        private static ICompilerReferenceError _CS0690;
        public static ICompilerReferenceError CS0692
        {
            get
            {
                if (_CS0692 == null)
                    _CS0692 = new CompilerReferenceError(Resources.CSharpErrors_CS0692, 692);
                return _CS0692;
            }
        }
        private static ICompilerReferenceError _CS0692;
        public static ICompilerReferenceError CS0694
        {
            get
            {
                if (_CS0694 == null)
                    _CS0694 = new CompilerReferenceError(Resources.CSharpErrors_CS0694, 694);
                return _CS0694;
            }
        }
        private static ICompilerReferenceError _CS0694;
        public static ICompilerReferenceError CS0695
        {
            get
            {
                if (_CS0695 == null)
                    _CS0695 = new CompilerReferenceError(Resources.CSharpErrors_CS0695, 695);
                return _CS0695;
            }
        }
        private static ICompilerReferenceError _CS0695;
        public static ICompilerReferenceError CS0698
        {
            get
            {
                if (_CS0698 == null)
                    _CS0698 = new CompilerReferenceError(Resources.CSharpErrors_CS0698, 698);
                return _CS0698;
            }
        }
        private static ICompilerReferenceError _CS0698;
        public static ICompilerReferenceError CS0699
        {
            get
            {
                if (_CS0699 == null)
                    _CS0699 = new CompilerReferenceError(Resources.CSharpErrors_CS0699, 699);
                return _CS0699;
            }
        }
        private static ICompilerReferenceError _CS0699;
        public static ICompilerReferenceError CS0701
        {
            get
            {
                if (_CS0701 == null)
                    _CS0701 = new CompilerReferenceError(Resources.CSharpErrors_CS0701, 701);
                return _CS0701;
            }
        }
        private static ICompilerReferenceError _CS0701;
        public static ICompilerReferenceError CS0702
        {
            get
            {
                if (_CS0702 == null)
                    _CS0702 = new CompilerReferenceError(Resources.CSharpErrors_CS0702, 702);
                return _CS0702;
            }
        }
        private static ICompilerReferenceError _CS0702;
        public static ICompilerReferenceError CS0703
        {
            get
            {
                if (_CS0703 == null)
                    _CS0703 = new CompilerReferenceError(Resources.CSharpErrors_CS0703, 703);
                return _CS0703;
            }
        }
        private static ICompilerReferenceError _CS0703;
        public static ICompilerReferenceError CS0704
        {
            get
            {
                if (_CS0704 == null)
                    _CS0704 = new CompilerReferenceError(Resources.CSharpErrors_CS0704, 704);
                return _CS0704;
            }
        }
        private static ICompilerReferenceError _CS0704;
        public static ICompilerReferenceError CS0706
        {
            get
            {
                if (_CS0706 == null)
                    _CS0706 = new CompilerReferenceError(Resources.CSharpErrors_CS0706, 706);
                return _CS0706;
            }
        }
        private static ICompilerReferenceError _CS0706;
        public static ICompilerReferenceError CS0708
        {
            get
            {
                if (_CS0708 == null)
                    _CS0708 = new CompilerReferenceError(Resources.CSharpErrors_CS0708, 708);
                return _CS0708;
            }
        }
        private static ICompilerReferenceError _CS0708;
        public static ICompilerReferenceError CS0709
        {
            get
            {
                if (_CS0709 == null)
                    _CS0709 = new CompilerReferenceError(Resources.CSharpErrors_CS0709, 709);
                return _CS0709;
            }
        }
        private static ICompilerReferenceError _CS0709;
        public static ICompilerReferenceError CS0710
        {
            get
            {
                if (_CS0710 == null)
                    _CS0710 = new CompilerReferenceError(Resources.CSharpErrors_CS0710, 710);
                return _CS0710;
            }
        }
        private static ICompilerReferenceError _CS0710;
        public static ICompilerReferenceError CS0711
        {
            get
            {
                if (_CS0711 == null)
                    _CS0711 = new CompilerReferenceError(Resources.CSharpErrors_CS0711, 711);
                return _CS0711;
            }
        }
        private static ICompilerReferenceError _CS0711;
        public static ICompilerReferenceError CS0712
        {
            get
            {
                if (_CS0712 == null)
                    _CS0712 = new CompilerReferenceError(Resources.CSharpErrors_CS0712, 712);
                return _CS0712;
            }
        }
        private static ICompilerReferenceError _CS0712;
        public static ICompilerReferenceError CS0713
        {
            get
            {
                if (_CS0713 == null)
                    _CS0713 = new CompilerReferenceError(Resources.CSharpErrors_CS0713, 713);
                return _CS0713;
            }
        }
        private static ICompilerReferenceError _CS0713;
        public static ICompilerReferenceError CS0714
        {
            get
            {
                if (_CS0714 == null)
                    _CS0714 = new CompilerReferenceError(Resources.CSharpErrors_CS0714, 714);
                return _CS0714;
            }
        }
        private static ICompilerReferenceError _CS0714;
        public static ICompilerReferenceError CS0715
        {
            get
            {
                if (_CS0715 == null)
                    _CS0715 = new CompilerReferenceError(Resources.CSharpErrors_CS0715, 715);
                return _CS0715;
            }
        }
        private static ICompilerReferenceError _CS0715;
        public static ICompilerReferenceError CS0716
        {
            get
            {
                if (_CS0716 == null)
                    _CS0716 = new CompilerReferenceError(Resources.CSharpErrors_CS0716, 716);
                return _CS0716;
            }
        }
        private static ICompilerReferenceError _CS0716;
        public static ICompilerReferenceError CS0717
        {
            get
            {
                if (_CS0717 == null)
                    _CS0717 = new CompilerReferenceError(Resources.CSharpErrors_CS0717, 717);
                return _CS0717;
            }
        }
        private static ICompilerReferenceError _CS0717;
        public static ICompilerReferenceError CS0718
        {
            get
            {
                if (_CS0718 == null)
                    _CS0718 = new CompilerReferenceError(Resources.CSharpErrors_CS0718, 718);
                return _CS0718;
            }
        }
        private static ICompilerReferenceError _CS0718;
        public static ICompilerReferenceError CS0719
        {
            get
            {
                if (_CS0719 == null)
                    _CS0719 = new CompilerReferenceError(Resources.CSharpErrors_CS0719, 719);
                return _CS0719;
            }
        }
        private static ICompilerReferenceError _CS0719;
        public static ICompilerReferenceError CS0720
        {
            get
            {
                if (_CS0720 == null)
                    _CS0720 = new CompilerReferenceError(Resources.CSharpErrors_CS0720, 720);
                return _CS0720;
            }
        }
        private static ICompilerReferenceError _CS0720;
        public static ICompilerReferenceError CS0721
        {
            get
            {
                if (_CS0721 == null)
                    _CS0721 = new CompilerReferenceError(Resources.CSharpErrors_CS0721, 721);
                return _CS0721;
            }
        }
        private static ICompilerReferenceError _CS0721;
        public static ICompilerReferenceError CS0722
        {
            get
            {
                if (_CS0722 == null)
                    _CS0722 = new CompilerReferenceError(Resources.CSharpErrors_CS0722, 722);
                return _CS0722;
            }
        }
        private static ICompilerReferenceError _CS0722;
        public static ICompilerReferenceError CS0723
        {
            get
            {
                if (_CS0723 == null)
                    _CS0723 = new CompilerReferenceError(Resources.CSharpErrors_CS0723, 723);
                return _CS0723;
            }
        }
        private static ICompilerReferenceError _CS0723;
        public static ICompilerReferenceError CS0724
        {
            get
            {
                if (_CS0724 == null)
                    _CS0724 = new CompilerReferenceError(Resources.CSharpErrors_CS0724, 724);
                return _CS0724;
            }
        }
        private static ICompilerReferenceError _CS0724;
        public static ICompilerReferenceError CS0726
        {
            get
            {
                if (_CS0726 == null)
                    _CS0726 = new CompilerReferenceError(Resources.CSharpErrors_CS0726, 726);
                return _CS0726;
            }
        }
        private static ICompilerReferenceError _CS0726;
        public static ICompilerReferenceError CS0727
        {
            get
            {
                if (_CS0727 == null)
                    _CS0727 = new CompilerReferenceError(Resources.CSharpErrors_CS0727, 727);
                return _CS0727;
            }
        }
        private static ICompilerReferenceError _CS0727;
        public static ICompilerReferenceError CS0729
        {
            get
            {
                if (_CS0729 == null)
                    _CS0729 = new CompilerReferenceError(Resources.CSharpErrors_CS0729, 729);
                return _CS0729;
            }
        }
        private static ICompilerReferenceError _CS0729;
        public static ICompilerReferenceError CS0730
        {
            get
            {
                if (_CS0730 == null)
                    _CS0730 = new CompilerReferenceError(Resources.CSharpErrors_CS0730, 730);
                return _CS0730;
            }
        }
        private static ICompilerReferenceError _CS0730;
        public static ICompilerReferenceError CS0731
        {
            get
            {
                if (_CS0731 == null)
                    _CS0731 = new CompilerReferenceError(Resources.CSharpErrors_CS0731, 731);
                return _CS0731;
            }
        }
        private static ICompilerReferenceError _CS0731;
        public static ICompilerReferenceError CS0733
        {
            get
            {
                if (_CS0733 == null)
                    _CS0733 = new CompilerReferenceError(Resources.CSharpErrors_CS0733, 733);
                return _CS0733;
            }
        }
        private static ICompilerReferenceError _CS0733;
        public static ICompilerReferenceError CS0734
        {
            get
            {
                if (_CS0734 == null)
                    _CS0734 = new CompilerReferenceError(Resources.CSharpErrors_CS0734, 734);
                return _CS0734;
            }
        }
        private static ICompilerReferenceError _CS0734;
        public static ICompilerReferenceError CS0735
        {
            get
            {
                if (_CS0735 == null)
                    _CS0735 = new CompilerReferenceError(Resources.CSharpErrors_CS0735, 735);
                return _CS0735;
            }
        }
        private static ICompilerReferenceError _CS0735;
        public static ICompilerReferenceError CS0736
        {
            get
            {
                if (_CS0736 == null)
                    _CS0736 = new CompilerReferenceError(Resources.CSharpErrors_CS0736, 736);
                return _CS0736;
            }
        }
        private static ICompilerReferenceError _CS0736;
        public static ICompilerReferenceError CS0737
        {
            get
            {
                if (_CS0737 == null)
                    _CS0737 = new CompilerReferenceError(Resources.CSharpErrors_CS0737, 737);
                return _CS0737;
            }
        }
        private static ICompilerReferenceError _CS0737;
        public static ICompilerReferenceError CS0738
        {
            get
            {
                if (_CS0738 == null)
                    _CS0738 = new CompilerReferenceError(Resources.CSharpErrors_CS0738, 738);
                return _CS0738;
            }
        }
        private static ICompilerReferenceError _CS0738;
        public static ICompilerReferenceError CS0739
        {
            get
            {
                if (_CS0739 == null)
                    _CS0739 = new CompilerReferenceError(Resources.CSharpErrors_CS0739, 739);
                return _CS0739;
            }
        }
        private static ICompilerReferenceError _CS0739;
        public static ICompilerReferenceError CS0742
        {
            get
            {
                if (_CS0742 == null)
                    _CS0742 = new CompilerReferenceError(Resources.CSharpErrors_CS0742, 742);
                return _CS0742;
            }
        }
        private static ICompilerReferenceError _CS0742;
        public static ICompilerReferenceError CS0743
        {
            get
            {
                if (_CS0743 == null)
                    _CS0743 = new CompilerReferenceError(Resources.CSharpErrors_CS0743, 743);
                return _CS0743;
            }
        }
        private static ICompilerReferenceError _CS0743;
        public static ICompilerReferenceError CS0744
        {
            get
            {
                if (_CS0744 == null)
                    _CS0744 = new CompilerReferenceError(Resources.CSharpErrors_CS0744, 744);
                return _CS0744;
            }
        }
        private static ICompilerReferenceError _CS0744;
        public static ICompilerReferenceError CS0745
        {
            get
            {
                if (_CS0745 == null)
                    _CS0745 = new CompilerReferenceError(Resources.CSharpErrors_CS0745, 745);
                return _CS0745;
            }
        }
        private static ICompilerReferenceError _CS0745;
        public static ICompilerReferenceError CS0746
        {
            get
            {
                if (_CS0746 == null)
                    _CS0746 = new CompilerReferenceError(Resources.CSharpErrors_CS0746, 746);
                return _CS0746;
            }
        }
        private static ICompilerReferenceError _CS0746;
        public static ICompilerReferenceError CS0747
        {
            get
            {
                if (_CS0747 == null)
                    _CS0747 = new CompilerReferenceError(Resources.CSharpErrors_CS0747, 747);
                return _CS0747;
            }
        }
        private static ICompilerReferenceError _CS0747;
        public static ICompilerReferenceError CS0748
        {
            get
            {
                if (_CS0748 == null)
                    _CS0748 = new CompilerReferenceError(Resources.CSharpErrors_CS0748, 748);
                return _CS0748;
            }
        }
        private static ICompilerReferenceError _CS0748;
        public static ICompilerReferenceError CS0750
        {
            get
            {
                if (_CS0750 == null)
                    _CS0750 = new CompilerReferenceError(Resources.CSharpErrors_CS0750, 750);
                return _CS0750;
            }
        }
        private static ICompilerReferenceError _CS0750;
        public static ICompilerReferenceError CS0751
        {
            get
            {
                if (_CS0751 == null)
                    _CS0751 = new CompilerReferenceError(Resources.CSharpErrors_CS0751, 751);
                return _CS0751;
            }
        }
        private static ICompilerReferenceError _CS0751;
        public static ICompilerReferenceError CS0752
        {
            get
            {
                if (_CS0752 == null)
                    _CS0752 = new CompilerReferenceError(Resources.CSharpErrors_CS0752, 752);
                return _CS0752;
            }
        }
        private static ICompilerReferenceError _CS0752;
        public static ICompilerReferenceError CS0753
        {
            get
            {
                if (_CS0753 == null)
                    _CS0753 = new CompilerReferenceError(Resources.CSharpErrors_CS0753, 753);
                return _CS0753;
            }
        }
        private static ICompilerReferenceError _CS0753;
        public static ICompilerReferenceError CS0754
        {
            get
            {
                if (_CS0754 == null)
                    _CS0754 = new CompilerReferenceError(Resources.CSharpErrors_CS0754, 754);
                return _CS0754;
            }
        }
        private static ICompilerReferenceError _CS0754;
        public static ICompilerReferenceError CS0755
        {
            get
            {
                if (_CS0755 == null)
                    _CS0755 = new CompilerReferenceError(Resources.CSharpErrors_CS0755, 755);
                return _CS0755;
            }
        }
        private static ICompilerReferenceError _CS0755;
        public static ICompilerReferenceError CS0756
        {
            get
            {
                if (_CS0756 == null)
                    _CS0756 = new CompilerReferenceError(Resources.CSharpErrors_CS0756, 756);
                return _CS0756;
            }
        }
        private static ICompilerReferenceError _CS0756;
        public static ICompilerReferenceError CS0757
        {
            get
            {
                if (_CS0757 == null)
                    _CS0757 = new CompilerReferenceError(Resources.CSharpErrors_CS0757, 757);
                return _CS0757;
            }
        }
        private static ICompilerReferenceError _CS0757;
        public static ICompilerReferenceError CS0758
        {
            get
            {
                if (_CS0758 == null)
                    _CS0758 = new CompilerReferenceError(Resources.CSharpErrors_CS0758, 758);
                return _CS0758;
            }
        }
        private static ICompilerReferenceError _CS0758;
        public static ICompilerReferenceError CS0759
        {
            get
            {
                if (_CS0759 == null)
                    _CS0759 = new CompilerReferenceError(Resources.CSharpErrors_CS0759, 759);
                return _CS0759;
            }
        }
        private static ICompilerReferenceError _CS0759;
        public static ICompilerReferenceError CS0761
        {
            get
            {
                if (_CS0761 == null)
                    _CS0761 = new CompilerReferenceError(Resources.CSharpErrors_CS0761, 761);
                return _CS0761;
            }
        }
        private static ICompilerReferenceError _CS0761;
        public static ICompilerReferenceError CS0762
        {
            get
            {
                if (_CS0762 == null)
                    _CS0762 = new CompilerReferenceError(Resources.CSharpErrors_CS0762, 762);
                return _CS0762;
            }
        }
        private static ICompilerReferenceError _CS0762;
        public static ICompilerReferenceError CS0763
        {
            get
            {
                if (_CS0763 == null)
                    _CS0763 = new CompilerReferenceError(Resources.CSharpErrors_CS0763, 763);
                return _CS0763;
            }
        }
        private static ICompilerReferenceError _CS0763;
        public static ICompilerReferenceError CS0764
        {
            get
            {
                if (_CS0764 == null)
                    _CS0764 = new CompilerReferenceError(Resources.CSharpErrors_CS0764, 764);
                return _CS0764;
            }
        }
        private static ICompilerReferenceError _CS0764;
        public static ICompilerReferenceError CS0765
        {
            get
            {
                if (_CS0765 == null)
                    _CS0765 = new CompilerReferenceError(Resources.CSharpErrors_CS0765, 765);
                return _CS0765;
            }
        }
        private static ICompilerReferenceError _CS0765;
        public static ICompilerReferenceError CS0766
        {
            get
            {
                if (_CS0766 == null)
                    _CS0766 = new CompilerReferenceError(Resources.CSharpErrors_CS0766, 766);
                return _CS0766;
            }
        }
        private static ICompilerReferenceError _CS0766;
        public static ICompilerReferenceError CS0811
        {
            get
            {
                if (_CS0811 == null)
                    _CS0811 = new CompilerReferenceError(Resources.CSharpErrors_CS0811, 811);
                return _CS0811;
            }
        }
        private static ICompilerReferenceError _CS0811;
        public static ICompilerReferenceError CS0815
        {
            get
            {
                if (_CS0815 == null)
                    _CS0815 = new CompilerReferenceError(Resources.CSharpErrors_CS0815, 815);
                return _CS0815;
            }
        }
        private static ICompilerReferenceError _CS0815;
        public static ICompilerReferenceError CS0818
        {
            get
            {
                if (_CS0818 == null)
                    _CS0818 = new CompilerReferenceError(Resources.CSharpErrors_CS0818, 818);
                return _CS0818;
            }
        }
        private static ICompilerReferenceError _CS0818;
        public static ICompilerReferenceError CS0819
        {
            get
            {
                if (_CS0819 == null)
                    _CS0819 = new CompilerReferenceError(Resources.CSharpErrors_CS0819, 819);
                return _CS0819;
            }
        }
        private static ICompilerReferenceError _CS0819;
        public static ICompilerReferenceError CS0820
        {
            get
            {
                if (_CS0820 == null)
                    _CS0820 = new CompilerReferenceError(Resources.CSharpErrors_CS0820, 820);
                return _CS0820;
            }
        }
        private static ICompilerReferenceError _CS0820;
        public static ICompilerReferenceError CS0821
        {
            get
            {
                if (_CS0821 == null)
                    _CS0821 = new CompilerReferenceError(Resources.CSharpErrors_CS0821, 821);
                return _CS0821;
            }
        }
        private static ICompilerReferenceError _CS0821;
        public static ICompilerReferenceError CS0822
        {
            get
            {
                if (_CS0822 == null)
                    _CS0822 = new CompilerReferenceError(Resources.CSharpErrors_CS0822, 822);
                return _CS0822;
            }
        }
        private static ICompilerReferenceError _CS0822;
        public static ICompilerReferenceError CS0825
        {
            get
            {
                if (_CS0825 == null)
                    _CS0825 = new CompilerReferenceError(Resources.CSharpErrors_CS0825, 825);
                return _CS0825;
            }
        }
        private static ICompilerReferenceError _CS0825;
        public static ICompilerReferenceError CS0826
        {
            get
            {
                if (_CS0826 == null)
                    _CS0826 = new CompilerReferenceError(Resources.CSharpErrors_CS0826, 826);
                return _CS0826;
            }
        }
        private static ICompilerReferenceError _CS0826;
        public static ICompilerReferenceError CS0828
        {
            get
            {
                if (_CS0828 == null)
                    _CS0828 = new CompilerReferenceError(Resources.CSharpErrors_CS0828, 828);
                return _CS0828;
            }
        }
        private static ICompilerReferenceError _CS0828;
        public static ICompilerReferenceError CS0831
        {
            get
            {
                if (_CS0831 == null)
                    _CS0831 = new CompilerReferenceError(Resources.CSharpErrors_CS0831, 831);
                return _CS0831;
            }
        }
        private static ICompilerReferenceError _CS0831;
        public static ICompilerReferenceError CS0832
        {
            get
            {
                if (_CS0832 == null)
                    _CS0832 = new CompilerReferenceError(Resources.CSharpErrors_CS0832, 832);
                return _CS0832;
            }
        }
        private static ICompilerReferenceError _CS0832;
        public static ICompilerReferenceError CS0833
        {
            get
            {
                if (_CS0833 == null)
                    _CS0833 = new CompilerReferenceError(Resources.CSharpErrors_CS0833, 833);
                return _CS0833;
            }
        }
        private static ICompilerReferenceError _CS0833;
        public static ICompilerReferenceError CS0834
        {
            get
            {
                if (_CS0834 == null)
                    _CS0834 = new CompilerReferenceError(Resources.CSharpErrors_CS0834, 834);
                return _CS0834;
            }
        }
        private static ICompilerReferenceError _CS0834;
        public static ICompilerReferenceError CS0835
        {
            get
            {
                if (_CS0835 == null)
                    _CS0835 = new CompilerReferenceError(Resources.CSharpErrors_CS0835, 835);
                return _CS0835;
            }
        }
        private static ICompilerReferenceError _CS0835;
        public static ICompilerReferenceError CS0836
        {
            get
            {
                if (_CS0836 == null)
                    _CS0836 = new CompilerReferenceError(Resources.CSharpErrors_CS0836, 836);
                return _CS0836;
            }
        }
        private static ICompilerReferenceError _CS0836;
        public static ICompilerReferenceError CS0837
        {
            get
            {
                if (_CS0837 == null)
                    _CS0837 = new CompilerReferenceError(Resources.CSharpErrors_CS0837, 837);
                return _CS0837;
            }
        }
        private static ICompilerReferenceError _CS0837;
        public static ICompilerReferenceError CS0838
        {
            get
            {
                if (_CS0838 == null)
                    _CS0838 = new CompilerReferenceError(Resources.CSharpErrors_CS0838, 838);
                return _CS0838;
            }
        }
        private static ICompilerReferenceError _CS0838;
        public static ICompilerReferenceError CS0839
        {
            get
            {
                if (_CS0839 == null)
                    _CS0839 = new CompilerReferenceError(Resources.CSharpErrors_CS0839, 839);
                return _CS0839;
            }
        }
        private static ICompilerReferenceError _CS0839;
        public static ICompilerReferenceError CS0840
        {
            get
            {
                if (_CS0840 == null)
                    _CS0840 = new CompilerReferenceError(Resources.CSharpErrors_CS0840, 840);
                return _CS0840;
            }
        }
        private static ICompilerReferenceError _CS0840;
        public static ICompilerReferenceError CS0841
        {
            get
            {
                if (_CS0841 == null)
                    _CS0841 = new CompilerReferenceError(Resources.CSharpErrors_CS0841, 841);
                return _CS0841;
            }
        }
        private static ICompilerReferenceError _CS0841;
        public static ICompilerReferenceError CS0842
        {
            get
            {
                if (_CS0842 == null)
                    _CS0842 = new CompilerReferenceError(Resources.CSharpErrors_CS0842, 842);
                return _CS0842;
            }
        }
        private static ICompilerReferenceError _CS0842;
        public static ICompilerReferenceError CS0843
        {
            get
            {
                if (_CS0843 == null)
                    _CS0843 = new CompilerReferenceError(Resources.CSharpErrors_CS0843, 843);
                return _CS0843;
            }
        }
        private static ICompilerReferenceError _CS0843;
        public static ICompilerReferenceError CS0844
        {
            get
            {
                if (_CS0844 == null)
                    _CS0844 = new CompilerReferenceError(Resources.CSharpErrors_CS0844, 844);
                return _CS0844;
            }
        }
        private static ICompilerReferenceError _CS0844;
        public static ICompilerReferenceError CS0845
        {
            get
            {
                if (_CS0845 == null)
                    _CS0845 = new CompilerReferenceError(Resources.CSharpErrors_CS0845, 845);
                return _CS0845;
            }
        }
        private static ICompilerReferenceError _CS0845;
        public static ICompilerReferenceError CS1001
        {
            get
            {
                if (_CS1001 == null)
                    _CS1001 = new CompilerReferenceError(Resources.CSharpErrors_CS1001, 1001);
                return _CS1001;
            }
        }
        private static ICompilerReferenceError _CS1001;
        public static ICompilerReferenceError CS1002
        {
            get
            {
                if (_CS1002 == null)
                    _CS1002 = new CompilerReferenceError(Resources.CSharpErrors_CS1002, 1002);
                return _CS1002;
            }
        }
        private static ICompilerReferenceError _CS1002;
        public static ICompilerReferenceError CS1003
        {
            get
            {
                if (_CS1003 == null)
                    _CS1003 = new CompilerReferenceError(Resources.CSharpErrors_CS1003, 1003);
                return _CS1003;
            }
        }
        private static ICompilerReferenceError _CS1003;
        public static ICompilerReferenceError CS1004
        {
            get
            {
                if (_CS1004 == null)
                    _CS1004 = new CompilerReferenceError(Resources.CSharpErrors_CS1004, 1004);
                return _CS1004;
            }
        }
        private static ICompilerReferenceError _CS1004;
        public static ICompilerReferenceError CS1007
        {
            get
            {
                if (_CS1007 == null)
                    _CS1007 = new CompilerReferenceError(Resources.CSharpErrors_CS1007, 1007);
                return _CS1007;
            }
        }
        private static ICompilerReferenceError _CS1007;
        public static ICompilerReferenceError CS1008
        {
            get
            {
                if (_CS1008 == null)
                    _CS1008 = new CompilerReferenceError(Resources.CSharpErrors_CS1008, 1008);
                return _CS1008;
            }
        }
        private static ICompilerReferenceError _CS1008;
        public static ICompilerReferenceError CS1009
        {
            get
            {
                if (_CS1009 == null)
                    _CS1009 = new CompilerReferenceError(Resources.CSharpErrors_CS1009, 1009);
                return _CS1009;
            }
        }
        private static ICompilerReferenceError _CS1009;
        public static ICompilerReferenceError CS1010
        {
            get
            {
                if (_CS1010 == null)
                    _CS1010 = new CompilerReferenceError(Resources.CSharpErrors_CS1010, 1010);
                return _CS1010;
            }
        }
        private static ICompilerReferenceError _CS1010;
        public static ICompilerReferenceError CS1011
        {
            get
            {
                if (_CS1011 == null)
                    _CS1011 = new CompilerReferenceError(Resources.CSharpErrors_CS1011, 1011);
                return _CS1011;
            }
        }
        private static ICompilerReferenceError _CS1011;
        public static ICompilerReferenceError CS1012
        {
            get
            {
                if (_CS1012 == null)
                    _CS1012 = new CompilerReferenceError(Resources.CSharpErrors_CS1012, 1012);
                return _CS1012;
            }
        }
        private static ICompilerReferenceError _CS1012;
        public static ICompilerReferenceError CS1013
        {
            get
            {
                if (_CS1013 == null)
                    _CS1013 = new CompilerReferenceError(Resources.CSharpErrors_CS1013, 1013);
                return _CS1013;
            }
        }
        private static ICompilerReferenceError _CS1013;
        public static ICompilerReferenceError CS1014
        {
            get
            {
                if (_CS1014 == null)
                    _CS1014 = new CompilerReferenceError(Resources.CSharpErrors_CS1014, 1014);
                return _CS1014;
            }
        }
        private static ICompilerReferenceError _CS1014;
        public static ICompilerReferenceError CS1015
        {
            get
            {
                if (_CS1015 == null)
                    _CS1015 = new CompilerReferenceError(Resources.CSharpErrors_CS1015, 1015);
                return _CS1015;
            }
        }
        private static ICompilerReferenceError _CS1015;
        public static ICompilerReferenceError CS1016
        {
            get
            {
                if (_CS1016 == null)
                    _CS1016 = new CompilerReferenceError(Resources.CSharpErrors_CS1016, 1016);
                return _CS1016;
            }
        }
        private static ICompilerReferenceError _CS1016;
        public static ICompilerReferenceError CS1017
        {
            get
            {
                if (_CS1017 == null)
                    _CS1017 = new CompilerReferenceError(Resources.CSharpErrors_CS1017, 1017);
                return _CS1017;
            }
        }
        private static ICompilerReferenceError _CS1017;
        public static ICompilerReferenceError CS1018
        {
            get
            {
                if (_CS1018 == null)
                    _CS1018 = new CompilerReferenceError(Resources.CSharpErrors_CS1018, 1018);
                return _CS1018;
            }
        }
        private static ICompilerReferenceError _CS1018;
        public static ICompilerReferenceError CS1019
        {
            get
            {
                if (_CS1019 == null)
                    _CS1019 = new CompilerReferenceError(Resources.CSharpErrors_CS1019, 1019);
                return _CS1019;
            }
        }
        private static ICompilerReferenceError _CS1019;
        public static ICompilerReferenceError CS1020
        {
            get
            {
                if (_CS1020 == null)
                    _CS1020 = new CompilerReferenceError(Resources.CSharpErrors_CS1020, 1020);
                return _CS1020;
            }
        }
        private static ICompilerReferenceError _CS1020;
        public static ICompilerReferenceError CS1021
        {
            get
            {
                if (_CS1021 == null)
                    _CS1021 = new CompilerReferenceError(Resources.CSharpErrors_CS1021, 1021);
                return _CS1021;
            }
        }
        private static ICompilerReferenceError _CS1021;
        public static ICompilerReferenceError CS1022
        {
            get
            {
                if (_CS1022 == null)
                    _CS1022 = new CompilerReferenceError(Resources.CSharpErrors_CS1022, 1022);
                return _CS1022;
            }
        }
        private static ICompilerReferenceError _CS1022;
        public static ICompilerReferenceError CS1023
        {
            get
            {
                if (_CS1023 == null)
                    _CS1023 = new CompilerReferenceError(Resources.CSharpErrors_CS1023, 1023);
                return _CS1023;
            }
        }
        private static ICompilerReferenceError _CS1023;
        public static ICompilerReferenceError CS1024
        {
            get
            {
                if (_CS1024 == null)
                    _CS1024 = new CompilerReferenceError(Resources.CSharpErrors_CS1024, 1024);
                return _CS1024;
            }
        }
        private static ICompilerReferenceError _CS1024;
        public static ICompilerReferenceError CS1025
        {
            get
            {
                if (_CS1025 == null)
                    _CS1025 = new CompilerReferenceError(Resources.CSharpErrors_CS1025, 1025);
                return _CS1025;
            }
        }
        private static ICompilerReferenceError _CS1025;
        public static ICompilerReferenceError CS1026
        {
            get
            {
                if (_CS1026 == null)
                    _CS1026 = new CompilerReferenceError(Resources.CSharpErrors_CS1026, 1026);
                return _CS1026;
            }
        }
        private static ICompilerReferenceError _CS1026;
        public static ICompilerReferenceError CS1027
        {
            get
            {
                if (_CS1027 == null)
                    _CS1027 = new CompilerReferenceError(Resources.CSharpErrors_CS1027, 1027);
                return _CS1027;
            }
        }
        private static ICompilerReferenceError _CS1027;
        public static ICompilerReferenceError CS1028
        {
            get
            {
                if (_CS1028 == null)
                    _CS1028 = new CompilerReferenceError(Resources.CSharpErrors_CS1028, 1028);
                return _CS1028;
            }
        }
        private static ICompilerReferenceError _CS1028;
        public static ICompilerReferenceError CS1029
        {
            get
            {
                if (_CS1029 == null)
                    _CS1029 = new CompilerReferenceError(Resources.CSharpErrors_CS1029, 1029);
                return _CS1029;
            }
        }
        private static ICompilerReferenceError _CS1029;
        public static ICompilerReferenceError CS1031
        {
            get
            {
                if (_CS1031 == null)
                    _CS1031 = new CompilerReferenceError(Resources.CSharpErrors_CS1031, 1031);
                return _CS1031;
            }
        }
        private static ICompilerReferenceError _CS1031;
        public static ICompilerReferenceError CS1032
        {
            get
            {
                if (_CS1032 == null)
                    _CS1032 = new CompilerReferenceError(Resources.CSharpErrors_CS1032, 1032);
                return _CS1032;
            }
        }
        private static ICompilerReferenceError _CS1032;
        public static ICompilerReferenceError CS1033
        {
            get
            {
                if (_CS1033 == null)
                    _CS1033 = new CompilerReferenceError(Resources.CSharpErrors_CS1033, 1033);
                return _CS1033;
            }
        }
        private static ICompilerReferenceError _CS1033;
        public static ICompilerReferenceError CS1034
        {
            get
            {
                if (_CS1034 == null)
                    _CS1034 = new CompilerReferenceError(Resources.CSharpErrors_CS1034, 1034);
                return _CS1034;
            }
        }
        private static ICompilerReferenceError _CS1034;
        public static ICompilerReferenceError CS1035
        {
            get
            {
                if (_CS1035 == null)
                    _CS1035 = new CompilerReferenceError(Resources.CSharpErrors_CS1035, 1035);
                return _CS1035;
            }
        }
        private static ICompilerReferenceError _CS1035;
        public static ICompilerReferenceError CS1036
        {
            get
            {
                if (_CS1036 == null)
                    _CS1036 = new CompilerReferenceError(Resources.CSharpErrors_CS1036, 1036);
                return _CS1036;
            }
        }
        private static ICompilerReferenceError _CS1036;
        public static ICompilerReferenceError CS1037
        {
            get
            {
                if (_CS1037 == null)
                    _CS1037 = new CompilerReferenceError(Resources.CSharpErrors_CS1037, 1037);
                return _CS1037;
            }
        }
        private static ICompilerReferenceError _CS1037;
        public static ICompilerReferenceError CS1038
        {
            get
            {
                if (_CS1038 == null)
                    _CS1038 = new CompilerReferenceError(Resources.CSharpErrors_CS1038, 1038);
                return _CS1038;
            }
        }
        private static ICompilerReferenceError _CS1038;
        public static ICompilerReferenceError CS1039
        {
            get
            {
                if (_CS1039 == null)
                    _CS1039 = new CompilerReferenceError(Resources.CSharpErrors_CS1039, 1039);
                return _CS1039;
            }
        }
        private static ICompilerReferenceError _CS1039;
        public static ICompilerReferenceError CS1040
        {
            get
            {
                if (_CS1040 == null)
                    _CS1040 = new CompilerReferenceError(Resources.CSharpErrors_CS1040, 1040);
                return _CS1040;
            }
        }
        private static ICompilerReferenceError _CS1040;
        public static ICompilerReferenceError CS1041
        {
            get
            {
                if (_CS1041 == null)
                    _CS1041 = new CompilerReferenceError(Resources.CSharpErrors_CS1041, 1041);
                return _CS1041;
            }
        }
        private static ICompilerReferenceError _CS1041;
        public static ICompilerReferenceError CS1043
        {
            get
            {
                if (_CS1043 == null)
                    _CS1043 = new CompilerReferenceError(Resources.CSharpErrors_CS1043, 1043);
                return _CS1043;
            }
        }
        private static ICompilerReferenceError _CS1043;
        public static ICompilerReferenceError CS1044
        {
            get
            {
                if (_CS1044 == null)
                    _CS1044 = new CompilerReferenceError(Resources.CSharpErrors_CS1044, 1044);
                return _CS1044;
            }
        }
        private static ICompilerReferenceError _CS1044;
        public static ICompilerReferenceError CS1055
        {
            get
            {
                if (_CS1055 == null)
                    _CS1055 = new CompilerReferenceError(Resources.CSharpErrors_CS1055, 1055);
                return _CS1055;
            }
        }
        private static ICompilerReferenceError _CS1055;
        public static ICompilerReferenceError CS1056
        {
            get
            {
                if (_CS1056 == null)
                    _CS1056 = new CompilerReferenceError(Resources.CSharpErrors_CS1056, 1056);
                return _CS1056;
            }
        }
        private static ICompilerReferenceError _CS1056;
        public static ICompilerReferenceError CS1057
        {
            get
            {
                if (_CS1057 == null)
                    _CS1057 = new CompilerReferenceError(Resources.CSharpErrors_CS1057, 1057);
                return _CS1057;
            }
        }
        private static ICompilerReferenceError _CS1057;
        public static ICompilerReferenceError CS1059
        {
            get
            {
                if (_CS1059 == null)
                    _CS1059 = new CompilerReferenceError(Resources.CSharpErrors_CS1059, 1059);
                return _CS1059;
            }
        }
        private static ICompilerReferenceError _CS1059;
        public static ICompilerReferenceError CS1061
        {
            get
            {
                if (_CS1061 == null)
                    _CS1061 = new CompilerReferenceError(Resources.CSharpErrors_CS1061, 1061);
                return _CS1061;
            }
        }
        private static ICompilerReferenceError _CS1061;
        public static ICompilerReferenceError CS1100
        {
            get
            {
                if (_CS1100 == null)
                    _CS1100 = new CompilerReferenceError(Resources.CSharpErrors_CS1100, 1100);
                return _CS1100;
            }
        }
        private static ICompilerReferenceError _CS1100;
        public static ICompilerReferenceError CS1101
        {
            get
            {
                if (_CS1101 == null)
                    _CS1101 = new CompilerReferenceError(Resources.CSharpErrors_CS1101, 1101);
                return _CS1101;
            }
        }
        private static ICompilerReferenceError _CS1101;
        public static ICompilerReferenceError CS1102
        {
            get
            {
                if (_CS1102 == null)
                    _CS1102 = new CompilerReferenceError(Resources.CSharpErrors_CS1102, 1102);
                return _CS1102;
            }
        }
        private static ICompilerReferenceError _CS1102;
        public static ICompilerReferenceError CS1103
        {
            get
            {
                if (_CS1103 == null)
                    _CS1103 = new CompilerReferenceError(Resources.CSharpErrors_CS1103, 1103);
                return _CS1103;
            }
        }
        private static ICompilerReferenceError _CS1103;
        public static ICompilerReferenceError CS1104
        {
            get
            {
                if (_CS1104 == null)
                    _CS1104 = new CompilerReferenceError(Resources.CSharpErrors_CS1104, 1104);
                return _CS1104;
            }
        }
        private static ICompilerReferenceError _CS1104;
        public static ICompilerReferenceError CS1105
        {
            get
            {
                if (_CS1105 == null)
                    _CS1105 = new CompilerReferenceError(Resources.CSharpErrors_CS1105, 1105);
                return _CS1105;
            }
        }
        private static ICompilerReferenceError _CS1105;
        public static ICompilerReferenceError CS1106
        {
            get
            {
                if (_CS1106 == null)
                    _CS1106 = new CompilerReferenceError(Resources.CSharpErrors_CS1106, 1106);
                return _CS1106;
            }
        }
        private static ICompilerReferenceError _CS1106;
        public static ICompilerReferenceError CS1107
        {
            get
            {
                if (_CS1107 == null)
                    _CS1107 = new CompilerReferenceError(Resources.CSharpErrors_CS1107, 1107);
                return _CS1107;
            }
        }
        private static ICompilerReferenceError _CS1107;
        public static ICompilerReferenceError CS1108
        {
            get
            {
                if (_CS1108 == null)
                    _CS1108 = new CompilerReferenceError(Resources.CSharpErrors_CS1108, 1108);
                return _CS1108;
            }
        }
        private static ICompilerReferenceError _CS1108;
        public static ICompilerReferenceError CS1109
        {
            get
            {
                if (_CS1109 == null)
                    _CS1109 = new CompilerReferenceError(Resources.CSharpErrors_CS1109, 1109);
                return _CS1109;
            }
        }
        private static ICompilerReferenceError _CS1109;
        public static ICompilerReferenceError CS1110
        {
            get
            {
                if (_CS1110 == null)
                    _CS1110 = new CompilerReferenceError(Resources.CSharpErrors_CS1110, 1110);
                return _CS1110;
            }
        }
        private static ICompilerReferenceError _CS1110;
        public static ICompilerReferenceError CS1112
        {
            get
            {
                if (_CS1112 == null)
                    _CS1112 = new CompilerReferenceError(Resources.CSharpErrors_CS1112, 1112);
                return _CS1112;
            }
        }
        private static ICompilerReferenceError _CS1112;
        public static ICompilerReferenceError CS1113
        {
            get
            {
                if (_CS1113 == null)
                    _CS1113 = new CompilerReferenceError(Resources.CSharpErrors_CS1113, 1113);
                return _CS1113;
            }
        }
        private static ICompilerReferenceError _CS1113;
        public static ICompilerReferenceError CS1501
        {
            get
            {
                if (_CS1501 == null)
                    _CS1501 = new CompilerReferenceError(Resources.CSharpErrors_CS1501, 1501);
                return _CS1501;
            }
        }
        private static ICompilerReferenceError _CS1501;
        public static ICompilerReferenceError CS1502
        {
            get
            {
                if (_CS1502 == null)
                    _CS1502 = new CompilerReferenceError(Resources.CSharpErrors_CS1502, 1502);
                return _CS1502;
            }
        }
        private static ICompilerReferenceError _CS1502;
        public static ICompilerReferenceError CS1503
        {
            get
            {
                if (_CS1503 == null)
                    _CS1503 = new CompilerReferenceError(Resources.CSharpErrors_CS1503, 1503);
                return _CS1503;
            }
        }
        private static ICompilerReferenceError _CS1503;
        public static ICompilerReferenceError CS1504
        {
            get
            {
                if (_CS1504 == null)
                    _CS1504 = new CompilerReferenceError(Resources.CSharpErrors_CS1504, 1504);
                return _CS1504;
            }
        }
        private static ICompilerReferenceError _CS1504;
        public static ICompilerReferenceError CS1507
        {
            get
            {
                if (_CS1507 == null)
                    _CS1507 = new CompilerReferenceError(Resources.CSharpErrors_CS1507, 1507);
                return _CS1507;
            }
        }
        private static ICompilerReferenceError _CS1507;
        public static ICompilerReferenceError CS1508
        {
            get
            {
                if (_CS1508 == null)
                    _CS1508 = new CompilerReferenceError(Resources.CSharpErrors_CS1508, 1508);
                return _CS1508;
            }
        }
        private static ICompilerReferenceError _CS1508;
        public static ICompilerReferenceError CS1509
        {
            get
            {
                if (_CS1509 == null)
                    _CS1509 = new CompilerReferenceError(Resources.CSharpErrors_CS1509, 1509);
                return _CS1509;
            }
        }
        private static ICompilerReferenceError _CS1509;
        public static ICompilerReferenceError CS1510
        {
            get
            {
                if (_CS1510 == null)
                    _CS1510 = new CompilerReferenceError(Resources.CSharpErrors_CS1510, 1510);
                return _CS1510;
            }
        }
        private static ICompilerReferenceError _CS1510;
        public static ICompilerReferenceError CS1511
        {
            get
            {
                if (_CS1511 == null)
                    _CS1511 = new CompilerReferenceError(Resources.CSharpErrors_CS1511, 1511);
                return _CS1511;
            }
        }
        private static ICompilerReferenceError _CS1511;
        public static ICompilerReferenceError CS1512
        {
            get
            {
                if (_CS1512 == null)
                    _CS1512 = new CompilerReferenceError(Resources.CSharpErrors_CS1512, 1512);
                return _CS1512;
            }
        }
        private static ICompilerReferenceError _CS1512;
        public static ICompilerReferenceError CS1513
        {
            get
            {
                if (_CS1513 == null)
                    _CS1513 = new CompilerReferenceError(Resources.CSharpErrors_CS1513, 1513);
                return _CS1513;
            }
        }
        private static ICompilerReferenceError _CS1513;
        public static ICompilerReferenceError CS1514
        {
            get
            {
                if (_CS1514 == null)
                    _CS1514 = new CompilerReferenceError(Resources.CSharpErrors_CS1514, 1514);
                return _CS1514;
            }
        }
        private static ICompilerReferenceError _CS1514;
        public static ICompilerReferenceError CS1515
        {
            get
            {
                if (_CS1515 == null)
                    _CS1515 = new CompilerReferenceError(Resources.CSharpErrors_CS1515, 1515);
                return _CS1515;
            }
        }
        private static ICompilerReferenceError _CS1515;
        public static ICompilerReferenceError CS1517
        {
            get
            {
                if (_CS1517 == null)
                    _CS1517 = new CompilerReferenceError(Resources.CSharpErrors_CS1517, 1517);
                return _CS1517;
            }
        }
        private static ICompilerReferenceError _CS1517;
        public static ICompilerReferenceError CS1518
        {
            get
            {
                if (_CS1518 == null)
                    _CS1518 = new CompilerReferenceError(Resources.CSharpErrors_CS1518, 1518);
                return _CS1518;
            }
        }
        private static ICompilerReferenceError _CS1518;
        public static ICompilerReferenceError CS1519
        {
            get
            {
                if (_CS1519 == null)
                    _CS1519 = new CompilerReferenceError(Resources.CSharpErrors_CS1519, 1519);
                return _CS1519;
            }
        }
        private static ICompilerReferenceError _CS1519;
        public static ICompilerReferenceError CS1520
        {
            get
            {
                if (_CS1520 == null)
                    _CS1520 = new CompilerReferenceError(Resources.CSharpErrors_CS1520, 1520);
                return _CS1520;
            }
        }
        private static ICompilerReferenceError _CS1520;
        public static ICompilerReferenceError CS1521
        {
            get
            {
                if (_CS1521 == null)
                    _CS1521 = new CompilerReferenceError(Resources.CSharpErrors_CS1521, 1521);
                return _CS1521;
            }
        }
        private static ICompilerReferenceError _CS1521;
        public static ICompilerReferenceError CS1524
        {
            get
            {
                if (_CS1524 == null)
                    _CS1524 = new CompilerReferenceError(Resources.CSharpErrors_CS1524, 1524);
                return _CS1524;
            }
        }
        private static ICompilerReferenceError _CS1524;
        public static ICompilerReferenceError CS1525
        {
            get
            {
                if (_CS1525 == null)
                    _CS1525 = new CompilerReferenceError(Resources.CSharpErrors_CS1525, 1525);
                return _CS1525;
            }
        }
        private static ICompilerReferenceError _CS1525;
        public static ICompilerReferenceError CS1526
        {
            get
            {
                if (_CS1526 == null)
                    _CS1526 = new CompilerReferenceError(Resources.CSharpErrors_CS1526, 1526);
                return _CS1526;
            }
        }
        private static ICompilerReferenceError _CS1526;
        public static ICompilerReferenceError CS1527
        {
            get
            {
                if (_CS1527 == null)
                    _CS1527 = new CompilerReferenceError(Resources.CSharpErrors_CS1527, 1527);
                return _CS1527;
            }
        }
        private static ICompilerReferenceError _CS1527;
        public static ICompilerReferenceError CS1528
        {
            get
            {
                if (_CS1528 == null)
                    _CS1528 = new CompilerReferenceError(Resources.CSharpErrors_CS1528, 1528);
                return _CS1528;
            }
        }
        private static ICompilerReferenceError _CS1528;
        public static ICompilerReferenceError CS1529
        {
            get
            {
                if (_CS1529 == null)
                    _CS1529 = new CompilerReferenceError(Resources.CSharpErrors_CS1529, 1529);
                return _CS1529;
            }
        }
        private static ICompilerReferenceError _CS1529;
        public static ICompilerReferenceError CS1530
        {
            get
            {
                if (_CS1530 == null)
                    _CS1530 = new CompilerReferenceError(Resources.CSharpErrors_CS1530, 1530);
                return _CS1530;
            }
        }
        private static ICompilerReferenceError _CS1530;
        public static ICompilerReferenceError CS1534
        {
            get
            {
                if (_CS1534 == null)
                    _CS1534 = new CompilerReferenceError(Resources.CSharpErrors_CS1534, 1534);
                return _CS1534;
            }
        }
        private static ICompilerReferenceError _CS1534;
        public static ICompilerReferenceError CS1535
        {
            get
            {
                if (_CS1535 == null)
                    _CS1535 = new CompilerReferenceError(Resources.CSharpErrors_CS1535, 1535);
                return _CS1535;
            }
        }
        private static ICompilerReferenceError _CS1535;
        public static ICompilerReferenceError CS1536
        {
            get
            {
                if (_CS1536 == null)
                    _CS1536 = new CompilerReferenceError(Resources.CSharpErrors_CS1536, 1536);
                return _CS1536;
            }
        }
        private static ICompilerReferenceError _CS1536;
        public static ICompilerReferenceError CS1537
        {
            get
            {
                if (_CS1537 == null)
                    _CS1537 = new CompilerReferenceError(Resources.CSharpErrors_CS1537, 1537);
                return _CS1537;
            }
        }
        private static ICompilerReferenceError _CS1537;
        public static ICompilerReferenceError CS1540
        {
            get
            {
                if (_CS1540 == null)
                    _CS1540 = new CompilerReferenceError(Resources.CSharpErrors_CS1540, 1540);
                return _CS1540;
            }
        }
        private static ICompilerReferenceError _CS1540;
        public static ICompilerReferenceError CS1541
        {
            get
            {
                if (_CS1541 == null)
                    _CS1541 = new CompilerReferenceError(Resources.CSharpErrors_CS1541, 1541);
                return _CS1541;
            }
        }
        private static ICompilerReferenceError _CS1541;
        public static ICompilerReferenceError CS1542
        {
            get
            {
                if (_CS1542 == null)
                    _CS1542 = new CompilerReferenceError(Resources.CSharpErrors_CS1542, 1542);
                return _CS1542;
            }
        }
        private static ICompilerReferenceError _CS1542;
        public static ICompilerReferenceError CS1545
        {
            get
            {
                if (_CS1545 == null)
                    _CS1545 = new CompilerReferenceError(Resources.CSharpErrors_CS1545, 1545);
                return _CS1545;
            }
        }
        private static ICompilerReferenceError _CS1545;
        public static ICompilerReferenceError CS1546
        {
            get
            {
                if (_CS1546 == null)
                    _CS1546 = new CompilerReferenceError(Resources.CSharpErrors_CS1546, 1546);
                return _CS1546;
            }
        }
        private static ICompilerReferenceError _CS1546;
        public static ICompilerReferenceError CS1547
        {
            get
            {
                if (_CS1547 == null)
                    _CS1547 = new CompilerReferenceError(Resources.CSharpErrors_CS1547, 1547);
                return _CS1547;
            }
        }
        private static ICompilerReferenceError _CS1547;
        public static ICompilerReferenceError CS1548
        {
            get
            {
                if (_CS1548 == null)
                    _CS1548 = new CompilerReferenceError(Resources.CSharpErrors_CS1548, 1548);
                return _CS1548;
            }
        }
        private static ICompilerReferenceError _CS1548;
        public static ICompilerReferenceError CS1549
        {
            get
            {
                if (_CS1549 == null)
                    _CS1549 = new CompilerReferenceError(Resources.CSharpErrors_CS1549, 1549);
                return _CS1549;
            }
        }
        private static ICompilerReferenceError _CS1549;
        public static ICompilerReferenceError CS1551
        {
            get
            {
                if (_CS1551 == null)
                    _CS1551 = new CompilerReferenceError(Resources.CSharpErrors_CS1551, 1551);
                return _CS1551;
            }
        }
        private static ICompilerReferenceError _CS1551;
        public static ICompilerReferenceError CS1552
        {
            get
            {
                if (_CS1552 == null)
                    _CS1552 = new CompilerReferenceError(Resources.CSharpErrors_CS1552, 1552);
                return _CS1552;
            }
        }
        private static ICompilerReferenceError _CS1552;
        public static ICompilerReferenceError CS1553
        {
            get
            {
                if (_CS1553 == null)
                    _CS1553 = new CompilerReferenceError(Resources.CSharpErrors_CS1553, 1553);
                return _CS1553;
            }
        }
        private static ICompilerReferenceError _CS1553;
        public static ICompilerReferenceError CS1554
        {
            get
            {
                if (_CS1554 == null)
                    _CS1554 = new CompilerReferenceError(Resources.CSharpErrors_CS1554, 1554);
                return _CS1554;
            }
        }
        private static ICompilerReferenceError _CS1554;
        public static ICompilerReferenceError CS1555
        {
            get
            {
                if (_CS1555 == null)
                    _CS1555 = new CompilerReferenceError(Resources.CSharpErrors_CS1555, 1555);
                return _CS1555;
            }
        }
        private static ICompilerReferenceError _CS1555;
        public static ICompilerReferenceError CS1556
        {
            get
            {
                if (_CS1556 == null)
                    _CS1556 = new CompilerReferenceError(Resources.CSharpErrors_CS1556, 1556);
                return _CS1556;
            }
        }
        private static ICompilerReferenceError _CS1556;
        public static ICompilerReferenceError CS1557
        {
            get
            {
                if (_CS1557 == null)
                    _CS1557 = new CompilerReferenceError(Resources.CSharpErrors_CS1557, 1557);
                return _CS1557;
            }
        }
        private static ICompilerReferenceError _CS1557;
        public static ICompilerReferenceError CS1558
        {
            get
            {
                if (_CS1558 == null)
                    _CS1558 = new CompilerReferenceError(Resources.CSharpErrors_CS1558, 1558);
                return _CS1558;
            }
        }
        private static ICompilerReferenceError _CS1558;
        public static ICompilerReferenceError CS1559
        {
            get
            {
                if (_CS1559 == null)
                    _CS1559 = new CompilerReferenceError(Resources.CSharpErrors_CS1559, 1559);
                return _CS1559;
            }
        }
        private static ICompilerReferenceError _CS1559;
        public static ICompilerReferenceError CS1560
        {
            get
            {
                if (_CS1560 == null)
                    _CS1560 = new CompilerReferenceError(Resources.CSharpErrors_CS1560, 1560);
                return _CS1560;
            }
        }
        private static ICompilerReferenceError _CS1560;
        public static ICompilerReferenceError CS1561
        {
            get
            {
                if (_CS1561 == null)
                    _CS1561 = new CompilerReferenceError(Resources.CSharpErrors_CS1561, 1561);
                return _CS1561;
            }
        }
        private static ICompilerReferenceError _CS1561;
        public static ICompilerReferenceError CS1562
        {
            get
            {
                if (_CS1562 == null)
                    _CS1562 = new CompilerReferenceError(Resources.CSharpErrors_CS1562, 1562);
                return _CS1562;
            }
        }
        private static ICompilerReferenceError _CS1562;
        public static ICompilerReferenceError CS1563
        {
            get
            {
                if (_CS1563 == null)
                    _CS1563 = new CompilerReferenceError(Resources.CSharpErrors_CS1563, 1563);
                return _CS1563;
            }
        }
        private static ICompilerReferenceError _CS1563;
        public static ICompilerReferenceError CS1564
        {
            get
            {
                if (_CS1564 == null)
                    _CS1564 = new CompilerReferenceError(Resources.CSharpErrors_CS1564, 1564);
                return _CS1564;
            }
        }
        private static ICompilerReferenceError _CS1564;
        public static ICompilerReferenceError CS1565
        {
            get
            {
                if (_CS1565 == null)
                    _CS1565 = new CompilerReferenceError(Resources.CSharpErrors_CS1565, 1565);
                return _CS1565;
            }
        }
        private static ICompilerReferenceError _CS1565;
        public static ICompilerReferenceError CS1566
        {
            get
            {
                if (_CS1566 == null)
                    _CS1566 = new CompilerReferenceError(Resources.CSharpErrors_CS1566, 1566);
                return _CS1566;
            }
        }
        private static ICompilerReferenceError _CS1566;
        public static ICompilerReferenceError CS1567
        {
            get
            {
                if (_CS1567 == null)
                    _CS1567 = new CompilerReferenceError(Resources.CSharpErrors_CS1567, 1567);
                return _CS1567;
            }
        }
        private static ICompilerReferenceError _CS1567;
        public static ICompilerReferenceError CS1569
        {
            get
            {
                if (_CS1569 == null)
                    _CS1569 = new CompilerReferenceError(Resources.CSharpErrors_CS1569, 1569);
                return _CS1569;
            }
        }
        private static ICompilerReferenceError _CS1569;
        public static ICompilerReferenceError CS1575
        {
            get
            {
                if (_CS1575 == null)
                    _CS1575 = new CompilerReferenceError(Resources.CSharpErrors_CS1575, 1575);
                return _CS1575;
            }
        }
        private static ICompilerReferenceError _CS1575;
        public static ICompilerReferenceError CS1576
        {
            get
            {
                if (_CS1576 == null)
                    _CS1576 = new CompilerReferenceError(Resources.CSharpErrors_CS1576, 1576);
                return _CS1576;
            }
        }
        private static ICompilerReferenceError _CS1576;
        public static ICompilerReferenceError CS1577
        {
            get
            {
                if (_CS1577 == null)
                    _CS1577 = new CompilerReferenceError(Resources.CSharpErrors_CS1577, 1577);
                return _CS1577;
            }
        }
        private static ICompilerReferenceError _CS1577;
        public static ICompilerReferenceError CS1578
        {
            get
            {
                if (_CS1578 == null)
                    _CS1578 = new CompilerReferenceError(Resources.CSharpErrors_CS1578, 1578);
                return _CS1578;
            }
        }
        private static ICompilerReferenceError _CS1578;
        public static ICompilerReferenceError CS1579
        {
            get
            {
                if (_CS1579 == null)
                    _CS1579 = new CompilerReferenceError(Resources.CSharpErrors_CS1579, 1579);
                return _CS1579;
            }
        }
        private static ICompilerReferenceError _CS1579;
        public static ICompilerReferenceError CS1583
        {
            get
            {
                if (_CS1583 == null)
                    _CS1583 = new CompilerReferenceError(Resources.CSharpErrors_CS1583, 1583);
                return _CS1583;
            }
        }
        private static ICompilerReferenceError _CS1583;
        public static ICompilerReferenceError CS1585
        {
            get
            {
                if (_CS1585 == null)
                    _CS1585 = new CompilerReferenceError(Resources.CSharpErrors_CS1585, 1585);
                return _CS1585;
            }
        }
        private static ICompilerReferenceError _CS1585;
        public static ICompilerReferenceError CS1586
        {
            get
            {
                if (_CS1586 == null)
                    _CS1586 = new CompilerReferenceError(Resources.CSharpErrors_CS1586, 1586);
                return _CS1586;
            }
        }
        private static ICompilerReferenceError _CS1586;
        public static ICompilerReferenceError CS1588
        {
            get
            {
                if (_CS1588 == null)
                    _CS1588 = new CompilerReferenceError(Resources.CSharpErrors_CS1588, 1588);
                return _CS1588;
            }
        }
        private static ICompilerReferenceError _CS1588;
        public static ICompilerReferenceError CS1593
        {
            get
            {
                if (_CS1593 == null)
                    _CS1593 = new CompilerReferenceError(Resources.CSharpErrors_CS1593, 1593);
                return _CS1593;
            }
        }
        private static ICompilerReferenceError _CS1593;
        public static ICompilerReferenceError CS1594
        {
            get
            {
                if (_CS1594 == null)
                    _CS1594 = new CompilerReferenceError(Resources.CSharpErrors_CS1594, 1594);
                return _CS1594;
            }
        }
        private static ICompilerReferenceError _CS1594;
        public static ICompilerReferenceError CS1597
        {
            get
            {
                if (_CS1597 == null)
                    _CS1597 = new CompilerReferenceError(Resources.CSharpErrors_CS1597, 1597);
                return _CS1597;
            }
        }
        private static ICompilerReferenceError _CS1597;
        public static ICompilerReferenceError CS1599
        {
            get
            {
                if (_CS1599 == null)
                    _CS1599 = new CompilerReferenceError(Resources.CSharpErrors_CS1599, 1599);
                return _CS1599;
            }
        }
        private static ICompilerReferenceError _CS1599;
        public static ICompilerReferenceError CS1600
        {
            get
            {
                if (_CS1600 == null)
                    _CS1600 = new CompilerReferenceError(Resources.CSharpErrors_CS1600, 1600);
                return _CS1600;
            }
        }
        private static ICompilerReferenceError _CS1600;
        public static ICompilerReferenceError CS1601
        {
            get
            {
                if (_CS1601 == null)
                    _CS1601 = new CompilerReferenceError(Resources.CSharpErrors_CS1601, 1601);
                return _CS1601;
            }
        }
        private static ICompilerReferenceError _CS1601;
        public static ICompilerReferenceError CS1604
        {
            get
            {
                if (_CS1604 == null)
                    _CS1604 = new CompilerReferenceError(Resources.CSharpErrors_CS1604, 1604);
                return _CS1604;
            }
        }
        private static ICompilerReferenceError _CS1604;
        public static ICompilerReferenceError CS1605
        {
            get
            {
                if (_CS1605 == null)
                    _CS1605 = new CompilerReferenceError(Resources.CSharpErrors_CS1605, 1605);
                return _CS1605;
            }
        }
        private static ICompilerReferenceError _CS1605;
        public static ICompilerReferenceError CS1606
        {
            get
            {
                if (_CS1606 == null)
                    _CS1606 = new CompilerReferenceError(Resources.CSharpErrors_CS1606, 1606);
                return _CS1606;
            }
        }
        private static ICompilerReferenceError _CS1606;
        public static ICompilerReferenceError CS1608
        {
            get
            {
                if (_CS1608 == null)
                    _CS1608 = new CompilerReferenceError(Resources.CSharpErrors_CS1608, 1608);
                return _CS1608;
            }
        }
        private static ICompilerReferenceError _CS1608;
        public static ICompilerReferenceError CS1609
        {
            get
            {
                if (_CS1609 == null)
                    _CS1609 = new CompilerReferenceError(Resources.CSharpErrors_CS1609, 1609);
                return _CS1609;
            }
        }
        private static ICompilerReferenceError _CS1609;
        public static ICompilerReferenceError CS1611
        {
            get
            {
                if (_CS1611 == null)
                    _CS1611 = new CompilerReferenceError(Resources.CSharpErrors_CS1611, 1611);
                return _CS1611;
            }
        }
        private static ICompilerReferenceError _CS1611;
        public static ICompilerReferenceError CS1612
        {
            get
            {
                if (_CS1612 == null)
                    _CS1612 = new CompilerReferenceError(Resources.CSharpErrors_CS1612, 1612);
                return _CS1612;
            }
        }
        private static ICompilerReferenceError _CS1612;
        public static ICompilerReferenceError CS1613
        {
            get
            {
                if (_CS1613 == null)
                    _CS1613 = new CompilerReferenceError(Resources.CSharpErrors_CS1613, 1613);
                return _CS1613;
            }
        }
        private static ICompilerReferenceError _CS1613;
        public static ICompilerReferenceError CS1614
        {
            get
            {
                if (_CS1614 == null)
                    _CS1614 = new CompilerReferenceError(Resources.CSharpErrors_CS1614, 1614);
                return _CS1614;
            }
        }
        private static ICompilerReferenceError _CS1614;
        public static ICompilerReferenceError CS1615
        {
            get
            {
                if (_CS1615 == null)
                    _CS1615 = new CompilerReferenceError(Resources.CSharpErrors_CS1615, 1615);
                return _CS1615;
            }
        }
        private static ICompilerReferenceError _CS1615;
        public static ICompilerReferenceError CS1617
        {
            get
            {
                if (_CS1617 == null)
                    _CS1617 = new CompilerReferenceError(Resources.CSharpErrors_CS1617, 1617);
                return _CS1617;
            }
        }
        private static ICompilerReferenceError _CS1617;
        public static ICompilerReferenceError CS1618
        {
            get
            {
                if (_CS1618 == null)
                    _CS1618 = new CompilerReferenceError(Resources.CSharpErrors_CS1618, 1618);
                return _CS1618;
            }
        }
        private static ICompilerReferenceError _CS1618;
        public static ICompilerReferenceError CS1619
        {
            get
            {
                if (_CS1619 == null)
                    _CS1619 = new CompilerReferenceError(Resources.CSharpErrors_CS1619, 1619);
                return _CS1619;
            }
        }
        private static ICompilerReferenceError _CS1619;
        public static ICompilerReferenceError CS1620
        {
            get
            {
                if (_CS1620 == null)
                    _CS1620 = new CompilerReferenceError(Resources.CSharpErrors_CS1620, 1620);
                return _CS1620;
            }
        }
        private static ICompilerReferenceError _CS1620;
        public static ICompilerReferenceError CS1621
        {
            get
            {
                if (_CS1621 == null)
                    _CS1621 = new CompilerReferenceError(Resources.CSharpErrors_CS1621, 1621);
                return _CS1621;
            }
        }
        private static ICompilerReferenceError _CS1621;
        public static ICompilerReferenceError CS1622
        {
            get
            {
                if (_CS1622 == null)
                    _CS1622 = new CompilerReferenceError(Resources.CSharpErrors_CS1622, 1622);
                return _CS1622;
            }
        }
        private static ICompilerReferenceError _CS1622;
        public static ICompilerReferenceError CS1623
        {
            get
            {
                if (_CS1623 == null)
                    _CS1623 = new CompilerReferenceError(Resources.CSharpErrors_CS1623, 1623);
                return _CS1623;
            }
        }
        private static ICompilerReferenceError _CS1623;
        public static ICompilerReferenceError CS1624
        {
            get
            {
                if (_CS1624 == null)
                    _CS1624 = new CompilerReferenceError(Resources.CSharpErrors_CS1624, 1624);
                return _CS1624;
            }
        }
        private static ICompilerReferenceError _CS1624;
        public static ICompilerReferenceError CS1625
        {
            get
            {
                if (_CS1625 == null)
                    _CS1625 = new CompilerReferenceError(Resources.CSharpErrors_CS1625, 1625);
                return _CS1625;
            }
        }
        private static ICompilerReferenceError _CS1625;
        public static ICompilerReferenceError CS1626
        {
            get
            {
                if (_CS1626 == null)
                    _CS1626 = new CompilerReferenceError(Resources.CSharpErrors_CS1626, 1626);
                return _CS1626;
            }
        }
        private static ICompilerReferenceError _CS1626;
        public static ICompilerReferenceError CS1627
        {
            get
            {
                if (_CS1627 == null)
                    _CS1627 = new CompilerReferenceError(Resources.CSharpErrors_CS1627, 1627);
                return _CS1627;
            }
        }
        private static ICompilerReferenceError _CS1627;
        public static ICompilerReferenceError CS1628
        {
            get
            {
                if (_CS1628 == null)
                    _CS1628 = new CompilerReferenceError(Resources.CSharpErrors_CS1628, 1628);
                return _CS1628;
            }
        }
        private static ICompilerReferenceError _CS1628;
        public static ICompilerReferenceError CS1629
        {
            get
            {
                if (_CS1629 == null)
                    _CS1629 = new CompilerReferenceError(Resources.CSharpErrors_CS1629, 1629);
                return _CS1629;
            }
        }
        private static ICompilerReferenceError _CS1629;
        public static ICompilerReferenceError CS1630
        {
            get
            {
                if (_CS1630 == null)
                    _CS1630 = new CompilerReferenceError(Resources.CSharpErrors_CS1630, 1630);
                return _CS1630;
            }
        }
        private static ICompilerReferenceError _CS1630;
        public static ICompilerReferenceError CS1631
        {
            get
            {
                if (_CS1631 == null)
                    _CS1631 = new CompilerReferenceError(Resources.CSharpErrors_CS1631, 1631);
                return _CS1631;
            }
        }
        private static ICompilerReferenceError _CS1631;
        public static ICompilerReferenceError CS1632
        {
            get
            {
                if (_CS1632 == null)
                    _CS1632 = new CompilerReferenceError(Resources.CSharpErrors_CS1632, 1632);
                return _CS1632;
            }
        }
        private static ICompilerReferenceError _CS1632;
        public static ICompilerReferenceError CS1637
        {
            get
            {
                if (_CS1637 == null)
                    _CS1637 = new CompilerReferenceError(Resources.CSharpErrors_CS1637, 1637);
                return _CS1637;
            }
        }
        private static ICompilerReferenceError _CS1637;
        public static ICompilerReferenceError CS1638
        {
            get
            {
                if (_CS1638 == null)
                    _CS1638 = new CompilerReferenceError(Resources.CSharpErrors_CS1638, 1638);
                return _CS1638;
            }
        }
        private static ICompilerReferenceError _CS1638;
        public static ICompilerReferenceError CS1639
        {
            get
            {
                if (_CS1639 == null)
                    _CS1639 = new CompilerReferenceError(Resources.CSharpErrors_CS1639, 1639);
                return _CS1639;
            }
        }
        private static ICompilerReferenceError _CS1639;
        public static ICompilerReferenceError CS1640
        {
            get
            {
                if (_CS1640 == null)
                    _CS1640 = new CompilerReferenceError(Resources.CSharpErrors_CS1640, 1640);
                return _CS1640;
            }
        }
        private static ICompilerReferenceError _CS1640;
        public static ICompilerReferenceError CS1641
        {
            get
            {
                if (_CS1641 == null)
                    _CS1641 = new CompilerReferenceError(Resources.CSharpErrors_CS1641, 1641);
                return _CS1641;
            }
        }
        private static ICompilerReferenceError _CS1641;
        public static ICompilerReferenceError CS1642
        {
            get
            {
                if (_CS1642 == null)
                    _CS1642 = new CompilerReferenceError(Resources.CSharpErrors_CS1642, 1642);
                return _CS1642;
            }
        }
        private static ICompilerReferenceError _CS1642;
        public static ICompilerReferenceError CS1643
        {
            get
            {
                if (_CS1643 == null)
                    _CS1643 = new CompilerReferenceError(Resources.CSharpErrors_CS1643, 1643);
                return _CS1643;
            }
        }
        private static ICompilerReferenceError _CS1643;
        public static ICompilerReferenceError CS1644
        {
            get
            {
                if (_CS1644 == null)
                    _CS1644 = new CompilerReferenceError(Resources.CSharpErrors_CS1644, 1644);
                return _CS1644;
            }
        }
        private static ICompilerReferenceError _CS1644;
        public static ICompilerReferenceError CS1646
        {
            get
            {
                if (_CS1646 == null)
                    _CS1646 = new CompilerReferenceError(Resources.CSharpErrors_CS1646, 1646);
                return _CS1646;
            }
        }
        private static ICompilerReferenceError _CS1646;
        public static ICompilerReferenceError CS1647
        {
            get
            {
                if (_CS1647 == null)
                    _CS1647 = new CompilerReferenceError(Resources.CSharpErrors_CS1647, 1647);
                return _CS1647;
            }
        }
        private static ICompilerReferenceError _CS1647;
        public static ICompilerReferenceError CS1648
        {
            get
            {
                if (_CS1648 == null)
                    _CS1648 = new CompilerReferenceError(Resources.CSharpErrors_CS1648, 1648);
                return _CS1648;
            }
        }
        private static ICompilerReferenceError _CS1648;
        public static ICompilerReferenceError CS1649
        {
            get
            {
                if (_CS1649 == null)
                    _CS1649 = new CompilerReferenceError(Resources.CSharpErrors_CS1649, 1649);
                return _CS1649;
            }
        }
        private static ICompilerReferenceError _CS1649;
        public static ICompilerReferenceError CS1650
        {
            get
            {
                if (_CS1650 == null)
                    _CS1650 = new CompilerReferenceError(Resources.CSharpErrors_CS1650, 1650);
                return _CS1650;
            }
        }
        private static ICompilerReferenceError _CS1650;
        public static ICompilerReferenceError CS1651
        {
            get
            {
                if (_CS1651 == null)
                    _CS1651 = new CompilerReferenceError(Resources.CSharpErrors_CS1651, 1651);
                return _CS1651;
            }
        }
        private static ICompilerReferenceError _CS1651;
        public static ICompilerReferenceError CS1654
        {
            get
            {
                if (_CS1654 == null)
                    _CS1654 = new CompilerReferenceError(Resources.CSharpErrors_CS1654, 1654);
                return _CS1654;
            }
        }
        private static ICompilerReferenceError _CS1654;
        public static ICompilerReferenceError CS1655
        {
            get
            {
                if (_CS1655 == null)
                    _CS1655 = new CompilerReferenceError(Resources.CSharpErrors_CS1655, 1655);
                return _CS1655;
            }
        }
        private static ICompilerReferenceError _CS1655;
        public static ICompilerReferenceError CS1656
        {
            get
            {
                if (_CS1656 == null)
                    _CS1656 = new CompilerReferenceError(Resources.CSharpErrors_CS1656, 1656);
                return _CS1656;
            }
        }
        private static ICompilerReferenceError _CS1656;
        public static ICompilerReferenceError CS1657
        {
            get
            {
                if (_CS1657 == null)
                    _CS1657 = new CompilerReferenceError(Resources.CSharpErrors_CS1657, 1657);
                return _CS1657;
            }
        }
        private static ICompilerReferenceError _CS1657;
        public static ICompilerReferenceError CS1660
        {
            get
            {
                if (_CS1660 == null)
                    _CS1660 = new CompilerReferenceError(Resources.CSharpErrors_CS1660, 1660);
                return _CS1660;
            }
        }
        private static ICompilerReferenceError _CS1660;
        public static ICompilerReferenceError CS1661
        {
            get
            {
                if (_CS1661 == null)
                    _CS1661 = new CompilerReferenceError(Resources.CSharpErrors_CS1661, 1661);
                return _CS1661;
            }
        }
        private static ICompilerReferenceError _CS1661;
        public static ICompilerReferenceError CS1662
        {
            get
            {
                if (_CS1662 == null)
                    _CS1662 = new CompilerReferenceError(Resources.CSharpErrors_CS1662, 1662);
                return _CS1662;
            }
        }
        private static ICompilerReferenceError _CS1662;
        public static ICompilerReferenceError CS1663
        {
            get
            {
                if (_CS1663 == null)
                    _CS1663 = new CompilerReferenceError(Resources.CSharpErrors_CS1663, 1663);
                return _CS1663;
            }
        }
        private static ICompilerReferenceError _CS1663;
        public static ICompilerReferenceError CS1664
        {
            get
            {
                if (_CS1664 == null)
                    _CS1664 = new CompilerReferenceError(Resources.CSharpErrors_CS1664, 1664);
                return _CS1664;
            }
        }
        private static ICompilerReferenceError _CS1664;
        public static ICompilerReferenceError CS1665
        {
            get
            {
                if (_CS1665 == null)
                    _CS1665 = new CompilerReferenceError(Resources.CSharpErrors_CS1665, 1665);
                return _CS1665;
            }
        }
        private static ICompilerReferenceError _CS1665;
        public static ICompilerReferenceError CS1666
        {
            get
            {
                if (_CS1666 == null)
                    _CS1666 = new CompilerReferenceError(Resources.CSharpErrors_CS1666, 1666);
                return _CS1666;
            }
        }
        private static ICompilerReferenceError _CS1666;
        public static ICompilerReferenceError CS1667
        {
            get
            {
                if (_CS1667 == null)
                    _CS1667 = new CompilerReferenceError(Resources.CSharpErrors_CS1667, 1667);
                return _CS1667;
            }
        }
        private static ICompilerReferenceError _CS1667;
        public static ICompilerReferenceError CS1670
        {
            get
            {
                if (_CS1670 == null)
                    _CS1670 = new CompilerReferenceError(Resources.CSharpErrors_CS1670, 1670);
                return _CS1670;
            }
        }
        private static ICompilerReferenceError _CS1670;
        public static ICompilerReferenceError CS1671
        {
            get
            {
                if (_CS1671 == null)
                    _CS1671 = new CompilerReferenceError(Resources.CSharpErrors_CS1671, 1671);
                return _CS1671;
            }
        }
        private static ICompilerReferenceError _CS1671;
        public static ICompilerReferenceError CS1672
        {
            get
            {
                if (_CS1672 == null)
                    _CS1672 = new CompilerReferenceError(Resources.CSharpErrors_CS1672, 1672);
                return _CS1672;
            }
        }
        private static ICompilerReferenceError _CS1672;
        public static ICompilerReferenceError CS1673
        {
            get
            {
                if (_CS1673 == null)
                    _CS1673 = new CompilerReferenceError(Resources.CSharpErrors_CS1673, 1673);
                return _CS1673;
            }
        }
        private static ICompilerReferenceError _CS1673;
        public static ICompilerReferenceError CS1674
        {
            get
            {
                if (_CS1674 == null)
                    _CS1674 = new CompilerReferenceError(Resources.CSharpErrors_CS1674, 1674);
                return _CS1674;
            }
        }
        private static ICompilerReferenceError _CS1674;
        public static ICompilerReferenceError CS1675
        {
            get
            {
                if (_CS1675 == null)
                    _CS1675 = new CompilerReferenceError(Resources.CSharpErrors_CS1675, 1675);
                return _CS1675;
            }
        }
        private static ICompilerReferenceError _CS1675;
        public static ICompilerReferenceError CS1676
        {
            get
            {
                if (_CS1676 == null)
                    _CS1676 = new CompilerReferenceError(Resources.CSharpErrors_CS1676, 1676);
                return _CS1676;
            }
        }
        private static ICompilerReferenceError _CS1676;
        public static ICompilerReferenceError CS1677
        {
            get
            {
                if (_CS1677 == null)
                    _CS1677 = new CompilerReferenceError(Resources.CSharpErrors_CS1677, 1677);
                return _CS1677;
            }
        }
        private static ICompilerReferenceError _CS1677;
        public static ICompilerReferenceError CS1678
        {
            get
            {
                if (_CS1678 == null)
                    _CS1678 = new CompilerReferenceError(Resources.CSharpErrors_CS1678, 1678);
                return _CS1678;
            }
        }
        private static ICompilerReferenceError _CS1678;
        public static ICompilerReferenceError CS1679
        {
            get
            {
                if (_CS1679 == null)
                    _CS1679 = new CompilerReferenceError(Resources.CSharpErrors_CS1679, 1679);
                return _CS1679;
            }
        }
        private static ICompilerReferenceError _CS1679;
        public static ICompilerReferenceError CS1680
        {
            get
            {
                if (_CS1680 == null)
                    _CS1680 = new CompilerReferenceError(Resources.CSharpErrors_CS1680, 1680);
                return _CS1680;
            }
        }
        private static ICompilerReferenceError _CS1680;
        public static ICompilerReferenceError CS1681
        {
            get
            {
                if (_CS1681 == null)
                    _CS1681 = new CompilerReferenceError(Resources.CSharpErrors_CS1681, 1681);
                return _CS1681;
            }
        }
        private static ICompilerReferenceError _CS1681;
        public static ICompilerReferenceError CS1686
        {
            get
            {
                if (_CS1686 == null)
                    _CS1686 = new CompilerReferenceError(Resources.CSharpErrors_CS1686, 1686);
                return _CS1686;
            }
        }
        private static ICompilerReferenceError _CS1686;
        public static ICompilerReferenceError CS1688
        {
            get
            {
                if (_CS1688 == null)
                    _CS1688 = new CompilerReferenceError(Resources.CSharpErrors_CS1688, 1688);
                return _CS1688;
            }
        }
        private static ICompilerReferenceError _CS1688;
        public static ICompilerReferenceError CS1689
        {
            get
            {
                if (_CS1689 == null)
                    _CS1689 = new CompilerReferenceError(Resources.CSharpErrors_CS1689, 1689);
                return _CS1689;
            }
        }
        private static ICompilerReferenceError _CS1689;
        public static ICompilerReferenceError CS1703
        {
            get
            {
                if (_CS1703 == null)
                    _CS1703 = new CompilerReferenceError(Resources.CSharpErrors_CS1703, 1703);
                return _CS1703;
            }
        }
        private static ICompilerReferenceError _CS1703;
        public static ICompilerReferenceError CS1704
        {
            get
            {
                if (_CS1704 == null)
                    _CS1704 = new CompilerReferenceError(Resources.CSharpErrors_CS1704, 1704);
                return _CS1704;
            }
        }
        private static ICompilerReferenceError _CS1704;
        public static ICompilerReferenceError CS1705
        {
            get
            {
                if (_CS1705 == null)
                    _CS1705 = new CompilerReferenceError(Resources.CSharpErrors_CS1705, 1705);
                return _CS1705;
            }
        }
        private static ICompilerReferenceError _CS1705;
        public static ICompilerReferenceError CS1706
        {
            get
            {
                if (_CS1706 == null)
                    _CS1706 = new CompilerReferenceError(Resources.CSharpErrors_CS1706, 1706);
                return _CS1706;
            }
        }
        private static ICompilerReferenceError _CS1706;
        public static ICompilerReferenceError CS1708
        {
            get
            {
                if (_CS1708 == null)
                    _CS1708 = new CompilerReferenceError(Resources.CSharpErrors_CS1708, 1708);
                return _CS1708;
            }
        }
        private static ICompilerReferenceError _CS1708;
        public static ICompilerReferenceError CS1713
        {
            get
            {
                if (_CS1713 == null)
                    _CS1713 = new CompilerReferenceError(Resources.CSharpErrors_CS1713, 1713);
                return _CS1713;
            }
        }
        private static ICompilerReferenceError _CS1713;
        public static ICompilerReferenceError CS1714
        {
            get
            {
                if (_CS1714 == null)
                    _CS1714 = new CompilerReferenceError(Resources.CSharpErrors_CS1714, 1714);
                return _CS1714;
            }
        }
        private static ICompilerReferenceError _CS1714;
        public static ICompilerReferenceError CS1715
        {
            get
            {
                if (_CS1715 == null)
                    _CS1715 = new CompilerReferenceError(Resources.CSharpErrors_CS1715, 1715);
                return _CS1715;
            }
        }
        private static ICompilerReferenceError _CS1715;
        public static ICompilerReferenceError CS1716
        {
            get
            {
                if (_CS1716 == null)
                    _CS1716 = new CompilerReferenceError(Resources.CSharpErrors_CS1716, 1716);
                return _CS1716;
            }
        }
        private static ICompilerReferenceError _CS1716;
        public static ICompilerReferenceError CS1719
        {
            get
            {
                if (_CS1719 == null)
                    _CS1719 = new CompilerReferenceError(Resources.CSharpErrors_CS1719, 1719);
                return _CS1719;
            }
        }
        private static ICompilerReferenceError _CS1719;
        public static ICompilerReferenceError CS1721
        {
            get
            {
                if (_CS1721 == null)
                    _CS1721 = new CompilerReferenceError(Resources.CSharpErrors_CS1721, 1721);
                return _CS1721;
            }
        }
        private static ICompilerReferenceError _CS1721;
        public static ICompilerReferenceError CS1722
        {
            get
            {
                if (_CS1722 == null)
                    _CS1722 = new CompilerReferenceError(Resources.CSharpErrors_CS1722, 1722);
                return _CS1722;
            }
        }
        private static ICompilerReferenceError _CS1722;
        public static ICompilerReferenceError CS1724
        {
            get
            {
                if (_CS1724 == null)
                    _CS1724 = new CompilerReferenceError(Resources.CSharpErrors_CS1724, 1724);
                return _CS1724;
            }
        }
        private static ICompilerReferenceError _CS1724;
        public static ICompilerReferenceError CS1725
        {
            get
            {
                if (_CS1725 == null)
                    _CS1725 = new CompilerReferenceError(Resources.CSharpErrors_CS1725, 1725);
                return _CS1725;
            }
        }
        private static ICompilerReferenceError _CS1725;
        public static ICompilerReferenceError CS1726
        {
            get
            {
                if (_CS1726 == null)
                    _CS1726 = new CompilerReferenceError(Resources.CSharpErrors_CS1726, 1726);
                return _CS1726;
            }
        }
        private static ICompilerReferenceError _CS1726;
        public static ICompilerReferenceError CS1727
        {
            get
            {
                if (_CS1727 == null)
                    _CS1727 = new CompilerReferenceError(Resources.CSharpErrors_CS1727, 1727);
                return _CS1727;
            }
        }
        private static ICompilerReferenceError _CS1727;
        public static ICompilerReferenceError CS1728
        {
            get
            {
                if (_CS1728 == null)
                    _CS1728 = new CompilerReferenceError(Resources.CSharpErrors_CS1728, 1728);
                return _CS1728;
            }
        }
        private static ICompilerReferenceError _CS1728;
        public static ICompilerReferenceError CS1729
        {
            get
            {
                if (_CS1729 == null)
                    _CS1729 = new CompilerReferenceError(Resources.CSharpErrors_CS1729, 1729);
                return _CS1729;
            }
        }
        private static ICompilerReferenceError _CS1729;
        public static ICompilerReferenceError CS1730
        {
            get
            {
                if (_CS1730 == null)
                    _CS1730 = new CompilerReferenceError(Resources.CSharpErrors_CS1730, 1730);
                return _CS1730;
            }
        }
        private static ICompilerReferenceError _CS1730;
        public static ICompilerReferenceError CS1731
        {
            get
            {
                if (_CS1731 == null)
                    _CS1731 = new CompilerReferenceError(Resources.CSharpErrors_CS1731, 1731);
                return _CS1731;
            }
        }
        private static ICompilerReferenceError _CS1731;
        public static ICompilerReferenceError CS1732
        {
            get
            {
                if (_CS1732 == null)
                    _CS1732 = new CompilerReferenceError(Resources.CSharpErrors_CS1732, 1732);
                return _CS1732;
            }
        }
        private static ICompilerReferenceError _CS1732;
        public static ICompilerReferenceError CS1733
        {
            get
            {
                if (_CS1733 == null)
                    _CS1733 = new CompilerReferenceError(Resources.CSharpErrors_CS1733, 1733);
                return _CS1733;
            }
        }
        private static ICompilerReferenceError _CS1733;
        public static ICompilerReferenceError CS1900
        {
            get
            {
                if (_CS1900 == null)
                    _CS1900 = new CompilerReferenceError(Resources.CSharpErrors_CS1900, 1900);
                return _CS1900;
            }
        }
        private static ICompilerReferenceError _CS1900;
        public static ICompilerReferenceError CS1902
        {
            get
            {
                if (_CS1902 == null)
                    _CS1902 = new CompilerReferenceError(Resources.CSharpErrors_CS1902, 1902);
                return _CS1902;
            }
        }
        private static ICompilerReferenceError _CS1902;
        public static ICompilerReferenceError CS1906
        {
            get
            {
                if (_CS1906 == null)
                    _CS1906 = new CompilerReferenceError(Resources.CSharpErrors_CS1906, 1906);
                return _CS1906;
            }
        }
        private static ICompilerReferenceError _CS1906;
        public static ICompilerReferenceError CS1908
        {
            get
            {
                if (_CS1908 == null)
                    _CS1908 = new CompilerReferenceError(Resources.CSharpErrors_CS1908, 1908);
                return _CS1908;
            }
        }
        private static ICompilerReferenceError _CS1908;
        public static ICompilerReferenceError CS1909
        {
            get
            {
                if (_CS1909 == null)
                    _CS1909 = new CompilerReferenceError(Resources.CSharpErrors_CS1909, 1909);
                return _CS1909;
            }
        }
        private static ICompilerReferenceError _CS1909;
        public static ICompilerReferenceError CS1910
        {
            get
            {
                if (_CS1910 == null)
                    _CS1910 = new CompilerReferenceError(Resources.CSharpErrors_CS1910, 1910);
                return _CS1910;
            }
        }
        private static ICompilerReferenceError _CS1910;
        public static ICompilerReferenceError CS1912
        {
            get
            {
                if (_CS1912 == null)
                    _CS1912 = new CompilerReferenceError(Resources.CSharpErrors_CS1912, 1912);
                return _CS1912;
            }
        }
        private static ICompilerReferenceError _CS1912;
        public static ICompilerReferenceError CS1913
        {
            get
            {
                if (_CS1913 == null)
                    _CS1913 = new CompilerReferenceError(Resources.CSharpErrors_CS1913, 1913);
                return _CS1913;
            }
        }
        private static ICompilerReferenceError _CS1913;
        public static ICompilerReferenceError CS1914
        {
            get
            {
                if (_CS1914 == null)
                    _CS1914 = new CompilerReferenceError(Resources.CSharpErrors_CS1914, 1914);
                return _CS1914;
            }
        }
        private static ICompilerReferenceError _CS1914;
        public static ICompilerReferenceError CS1917
        {
            get
            {
                if (_CS1917 == null)
                    _CS1917 = new CompilerReferenceError(Resources.CSharpErrors_CS1917, 1917);
                return _CS1917;
            }
        }
        private static ICompilerReferenceError _CS1917;
        public static ICompilerReferenceError CS1918
        {
            get
            {
                if (_CS1918 == null)
                    _CS1918 = new CompilerReferenceError(Resources.CSharpErrors_CS1918, 1918);
                return _CS1918;
            }
        }
        private static ICompilerReferenceError _CS1918;
        public static ICompilerReferenceError CS1919
        {
            get
            {
                if (_CS1919 == null)
                    _CS1919 = new CompilerReferenceError(Resources.CSharpErrors_CS1919, 1919);
                return _CS1919;
            }
        }
        private static ICompilerReferenceError _CS1919;
        public static ICompilerReferenceError CS1920
        {
            get
            {
                if (_CS1920 == null)
                    _CS1920 = new CompilerReferenceError(Resources.CSharpErrors_CS1920, 1920);
                return _CS1920;
            }
        }
        private static ICompilerReferenceError _CS1920;
        public static ICompilerReferenceError CS1921
        {
            get
            {
                if (_CS1921 == null)
                    _CS1921 = new CompilerReferenceError(Resources.CSharpErrors_CS1921, 1921);
                return _CS1921;
            }
        }
        private static ICompilerReferenceError _CS1921;
        public static ICompilerReferenceError CS1922
        {
            get
            {
                if (_CS1922 == null)
                    _CS1922 = new CompilerReferenceError(Resources.CSharpErrors_CS1922, 1922);
                return _CS1922;
            }
        }
        private static ICompilerReferenceError _CS1922;
        public static ICompilerReferenceError CS1925
        {
            get
            {
                if (_CS1925 == null)
                    _CS1925 = new CompilerReferenceError(Resources.CSharpErrors_CS1925, 1925);
                return _CS1925;
            }
        }
        private static ICompilerReferenceError _CS1925;
        public static ICompilerReferenceError CS1926
        {
            get
            {
                if (_CS1926 == null)
                    _CS1926 = new CompilerReferenceError(Resources.CSharpErrors_CS1926, 1926);
                return _CS1926;
            }
        }
        private static ICompilerReferenceError _CS1926;
        public static ICompilerReferenceError CS1928
        {
            get
            {
                if (_CS1928 == null)
                    _CS1928 = new CompilerReferenceError(Resources.CSharpErrors_CS1928, 1928);
                return _CS1928;
            }
        }
        private static ICompilerReferenceError _CS1928;
        public static ICompilerReferenceError CS1929
        {
            get
            {
                if (_CS1929 == null)
                    _CS1929 = new CompilerReferenceError(Resources.CSharpErrors_CS1929, 1929);
                return _CS1929;
            }
        }
        private static ICompilerReferenceError _CS1929;
        public static ICompilerReferenceError CS1930
        {
            get
            {
                if (_CS1930 == null)
                    _CS1930 = new CompilerReferenceError(Resources.CSharpErrors_CS1930, 1930);
                return _CS1930;
            }
        }
        private static ICompilerReferenceError _CS1930;
        public static ICompilerReferenceError CS1931
        {
            get
            {
                if (_CS1931 == null)
                    _CS1931 = new CompilerReferenceError(Resources.CSharpErrors_CS1931, 1931);
                return _CS1931;
            }
        }
        private static ICompilerReferenceError _CS1931;
        public static ICompilerReferenceError CS1932
        {
            get
            {
                if (_CS1932 == null)
                    _CS1932 = new CompilerReferenceError(Resources.CSharpErrors_CS1932, 1932);
                return _CS1932;
            }
        }
        private static ICompilerReferenceError _CS1932;
        public static ICompilerReferenceError CS1933
        {
            get
            {
                if (_CS1933 == null)
                    _CS1933 = new CompilerReferenceError(Resources.CSharpErrors_CS1933, 1933);
                return _CS1933;
            }
        }
        private static ICompilerReferenceError _CS1933;
        public static ICompilerReferenceError CS1934
        {
            get
            {
                if (_CS1934 == null)
                    _CS1934 = new CompilerReferenceError(Resources.CSharpErrors_CS1934, 1934);
                return _CS1934;
            }
        }
        private static ICompilerReferenceError _CS1934;
        public static ICompilerReferenceError CS1935
        {
            get
            {
                if (_CS1935 == null)
                    _CS1935 = new CompilerReferenceError(Resources.CSharpErrors_CS1935, 1935);
                return _CS1935;
            }
        }
        private static ICompilerReferenceError _CS1935;
        public static ICompilerReferenceError CS1936
        {
            get
            {
                if (_CS1936 == null)
                    _CS1936 = new CompilerReferenceError(Resources.CSharpErrors_CS1936, 1936);
                return _CS1936;
            }
        }
        private static ICompilerReferenceError _CS1936;
        public static ICompilerReferenceError CS1937
        {
            get
            {
                if (_CS1937 == null)
                    _CS1937 = new CompilerReferenceError(Resources.CSharpErrors_CS1937, 1937);
                return _CS1937;
            }
        }
        private static ICompilerReferenceError _CS1937;
        public static ICompilerReferenceError CS1938
        {
            get
            {
                if (_CS1938 == null)
                    _CS1938 = new CompilerReferenceError(Resources.CSharpErrors_CS1938, 1938);
                return _CS1938;
            }
        }
        private static ICompilerReferenceError _CS1938;
        public static ICompilerReferenceError CS1939
        {
            get
            {
                if (_CS1939 == null)
                    _CS1939 = new CompilerReferenceError(Resources.CSharpErrors_CS1939, 1939);
                return _CS1939;
            }
        }
        private static ICompilerReferenceError _CS1939;
        public static ICompilerReferenceError CS1940
        {
            get
            {
                if (_CS1940 == null)
                    _CS1940 = new CompilerReferenceError(Resources.CSharpErrors_CS1940, 1940);
                return _CS1940;
            }
        }
        private static ICompilerReferenceError _CS1940;
        public static ICompilerReferenceError CS1941
        {
            get
            {
                if (_CS1941 == null)
                    _CS1941 = new CompilerReferenceError(Resources.CSharpErrors_CS1941, 1941);
                return _CS1941;
            }
        }
        private static ICompilerReferenceError _CS1941;
        public static ICompilerReferenceError CS1942
        {
            get
            {
                if (_CS1942 == null)
                    _CS1942 = new CompilerReferenceError(Resources.CSharpErrors_CS1942, 1942);
                return _CS1942;
            }
        }
        private static ICompilerReferenceError _CS1942;
        public static ICompilerReferenceError CS1943
        {
            get
            {
                if (_CS1943 == null)
                    _CS1943 = new CompilerReferenceError(Resources.CSharpErrors_CS1943, 1943);
                return _CS1943;
            }
        }
        private static ICompilerReferenceError _CS1943;
        public static ICompilerReferenceError CS1944
        {
            get
            {
                if (_CS1944 == null)
                    _CS1944 = new CompilerReferenceError(Resources.CSharpErrors_CS1944, 1944);
                return _CS1944;
            }
        }
        private static ICompilerReferenceError _CS1944;
        public static ICompilerReferenceError CS1945
        {
            get
            {
                if (_CS1945 == null)
                    _CS1945 = new CompilerReferenceError(Resources.CSharpErrors_CS1945, 1945);
                return _CS1945;
            }
        }
        private static ICompilerReferenceError _CS1945;
        public static ICompilerReferenceError CS1946
        {
            get
            {
                if (_CS1946 == null)
                    _CS1946 = new CompilerReferenceError(Resources.CSharpErrors_CS1946, 1946);
                return _CS1946;
            }
        }
        private static ICompilerReferenceError _CS1946;
        public static ICompilerReferenceError CS1947
        {
            get
            {
                if (_CS1947 == null)
                    _CS1947 = new CompilerReferenceError(Resources.CSharpErrors_CS1947, 1947);
                return _CS1947;
            }
        }
        private static ICompilerReferenceError _CS1947;
        public static ICompilerReferenceError CS1948
        {
            get
            {
                if (_CS1948 == null)
                    _CS1948 = new CompilerReferenceError(Resources.CSharpErrors_CS1948, 1948);
                return _CS1948;
            }
        }
        private static ICompilerReferenceError _CS1948;
        public static ICompilerReferenceError CS1949
        {
            get
            {
                if (_CS1949 == null)
                    _CS1949 = new CompilerReferenceError(Resources.CSharpErrors_CS1949, 1949);
                return _CS1949;
            }
        }
        private static ICompilerReferenceError _CS1949;
        public static ICompilerReferenceError CS1950
        {
            get
            {
                if (_CS1950 == null)
                    _CS1950 = new CompilerReferenceError(Resources.CSharpErrors_CS1950, 1950);
                return _CS1950;
            }
        }
        private static ICompilerReferenceError _CS1950;
        public static ICompilerReferenceError CS1951
        {
            get
            {
                if (_CS1951 == null)
                    _CS1951 = new CompilerReferenceError(Resources.CSharpErrors_CS1951, 1951);
                return _CS1951;
            }
        }
        private static ICompilerReferenceError _CS1951;
        public static ICompilerReferenceError CS1952
        {
            get
            {
                if (_CS1952 == null)
                    _CS1952 = new CompilerReferenceError(Resources.CSharpErrors_CS1952, 1952);
                return _CS1952;
            }
        }
        private static ICompilerReferenceError _CS1952;
        public static ICompilerReferenceError CS1953
        {
            get
            {
                if (_CS1953 == null)
                    _CS1953 = new CompilerReferenceError(Resources.CSharpErrors_CS1953, 1953);
                return _CS1953;
            }
        }
        private static ICompilerReferenceError _CS1953;
        public static ICompilerReferenceError CS1954
        {
            get
            {
                if (_CS1954 == null)
                    _CS1954 = new CompilerReferenceError(Resources.CSharpErrors_CS1954, 1954);
                return _CS1954;
            }
        }
        private static ICompilerReferenceError _CS1954;
        public static ICompilerReferenceError CS1955
        {
            get
            {
                if (_CS1955 == null)
                    _CS1955 = new CompilerReferenceError(Resources.CSharpErrors_CS1955, 1955);
                return _CS1955;
            }
        }
        private static ICompilerReferenceError _CS1955;
        public static ICompilerReferenceError CS1958
        {
            get
            {
                if (_CS1958 == null)
                    _CS1958 = new CompilerReferenceError(Resources.CSharpErrors_CS1958, 1958);
                return _CS1958;
            }
        }
        private static ICompilerReferenceError _CS1958;
        public static ICompilerReferenceError CS1959
        {
            get
            {
                if (_CS1959 == null)
                    _CS1959 = new CompilerReferenceError(Resources.CSharpErrors_CS1959, 1959);
                return _CS1959;
            }
        }
        private static ICompilerReferenceError _CS1959;
        public static ICompilerReferenceError CS2001
        {
            get
            {
                if (_CS2001 == null)
                    _CS2001 = new CompilerReferenceError(Resources.CSharpErrors_CS2001, 2001);
                return _CS2001;
            }
        }
        private static ICompilerReferenceError _CS2001;
        public static ICompilerReferenceError CS2003
        {
            get
            {
                if (_CS2003 == null)
                    _CS2003 = new CompilerReferenceError(Resources.CSharpErrors_CS2003, 2003);
                return _CS2003;
            }
        }
        private static ICompilerReferenceError _CS2003;
        public static ICompilerReferenceError CS2005
        {
            get
            {
                if (_CS2005 == null)
                    _CS2005 = new CompilerReferenceError(Resources.CSharpErrors_CS2005, 2005);
                return _CS2005;
            }
        }
        private static ICompilerReferenceError _CS2005;
        public static ICompilerReferenceError CS2006
        {
            get
            {
                if (_CS2006 == null)
                    _CS2006 = new CompilerReferenceError(Resources.CSharpErrors_CS2006, 2006);
                return _CS2006;
            }
        }
        private static ICompilerReferenceError _CS2006;
        public static ICompilerReferenceError CS2007
        {
            get
            {
                if (_CS2007 == null)
                    _CS2007 = new CompilerReferenceError(Resources.CSharpErrors_CS2007, 2007);
                return _CS2007;
            }
        }
        private static ICompilerReferenceError _CS2007;
        public static ICompilerReferenceError CS2008
        {
            get
            {
                if (_CS2008 == null)
                    _CS2008 = new CompilerReferenceError(Resources.CSharpErrors_CS2008, 2008);
                return _CS2008;
            }
        }
        private static ICompilerReferenceError _CS2008;
        public static ICompilerReferenceError CS2011
        {
            get
            {
                if (_CS2011 == null)
                    _CS2011 = new CompilerReferenceError(Resources.CSharpErrors_CS2011, 2011);
                return _CS2011;
            }
        }
        private static ICompilerReferenceError _CS2011;
        public static ICompilerReferenceError CS2012
        {
            get
            {
                if (_CS2012 == null)
                    _CS2012 = new CompilerReferenceError(Resources.CSharpErrors_CS2012, 2012);
                return _CS2012;
            }
        }
        private static ICompilerReferenceError _CS2012;
        public static ICompilerReferenceError CS2013
        {
            get
            {
                if (_CS2013 == null)
                    _CS2013 = new CompilerReferenceError(Resources.CSharpErrors_CS2013, 2013);
                return _CS2013;
            }
        }
        private static ICompilerReferenceError _CS2013;
        public static ICompilerReferenceError CS2015
        {
            get
            {
                if (_CS2015 == null)
                    _CS2015 = new CompilerReferenceError(Resources.CSharpErrors_CS2015, 2015);
                return _CS2015;
            }
        }
        private static ICompilerReferenceError _CS2015;
        public static ICompilerReferenceError CS2016
        {
            get
            {
                if (_CS2016 == null)
                    _CS2016 = new CompilerReferenceError(Resources.CSharpErrors_CS2016, 2016);
                return _CS2016;
            }
        }
        private static ICompilerReferenceError _CS2016;
        public static ICompilerReferenceError CS2017
        {
            get
            {
                if (_CS2017 == null)
                    _CS2017 = new CompilerReferenceError(Resources.CSharpErrors_CS2017, 2017);
                return _CS2017;
            }
        }
        private static ICompilerReferenceError _CS2017;
        public static ICompilerReferenceError CS2018
        {
            get
            {
                if (_CS2018 == null)
                    _CS2018 = new CompilerReferenceError(Resources.CSharpErrors_CS2018, 2018);
                return _CS2018;
            }
        }
        private static ICompilerReferenceError _CS2018;
        public static ICompilerReferenceError CS2019
        {
            get
            {
                if (_CS2019 == null)
                    _CS2019 = new CompilerReferenceError(Resources.CSharpErrors_CS2019, 2019);
                return _CS2019;
            }
        }
        private static ICompilerReferenceError _CS2019;
        public static ICompilerReferenceError CS2020
        {
            get
            {
                if (_CS2020 == null)
                    _CS2020 = new CompilerReferenceError(Resources.CSharpErrors_CS2020, 2020);
                return _CS2020;
            }
        }
        private static ICompilerReferenceError _CS2020;
        public static ICompilerReferenceError CS2021
        {
            get
            {
                if (_CS2021 == null)
                    _CS2021 = new CompilerReferenceError(Resources.CSharpErrors_CS2021, 2021);
                return _CS2021;
            }
        }
        private static ICompilerReferenceError _CS2021;
        public static ICompilerReferenceError CS2022
        {
            get
            {
                if (_CS2022 == null)
                    _CS2022 = new CompilerReferenceError(Resources.CSharpErrors_CS2022, 2022);
                return _CS2022;
            }
        }
        private static ICompilerReferenceError _CS2022;
        public static ICompilerReferenceError CS2024
        {
            get
            {
                if (_CS2024 == null)
                    _CS2024 = new CompilerReferenceError(Resources.CSharpErrors_CS2024, 2024);
                return _CS2024;
            }
        }
        private static ICompilerReferenceError _CS2024;
        public static ICompilerReferenceError CS2032
        {
            get
            {
                if (_CS2032 == null)
                    _CS2032 = new CompilerReferenceError(Resources.CSharpErrors_CS2032, 2032);
                return _CS2032;
            }
        }
        private static ICompilerReferenceError _CS2032;
        public static ICompilerReferenceError CS2033
        {
            get
            {
                if (_CS2033 == null)
                    _CS2033 = new CompilerReferenceError(Resources.CSharpErrors_CS2033, 2033);
                return _CS2033;
            }
        }
        private static ICompilerReferenceError _CS2033;
        public static ICompilerReferenceError CS2034
        {
            get
            {
                if (_CS2034 == null)
                    _CS2034 = new CompilerReferenceError(Resources.CSharpErrors_CS2034, 2034);
                return _CS2034;
            }
        }
        private static ICompilerReferenceError _CS2034;
        public static ICompilerReferenceError CS2035
        {
            get
            {
                if (_CS2035 == null)
                    _CS2035 = new CompilerReferenceError(Resources.CSharpErrors_CS2035, 2035);
                return _CS2035;
            }
        }
        private static ICompilerReferenceError _CS2035;
        public static ICompilerReferenceError CS2036
        {
            get
            {
                if (_CS2036 == null)
                    _CS2036 = new CompilerReferenceError(Resources.CSharpErrors_CS2036, 2036);
                return _CS2036;
            }
        }
        private static ICompilerReferenceError _CS2036;
        public static ICompilerReferenceError CS5001
        {
            get
            {
                if (_CS5001 == null)
                    _CS5001 = new CompilerReferenceError(Resources.CSharpErrors_CS5001, 5001);
                return _CS5001;
            }
        }
        private static ICompilerReferenceError _CS5001;
    }
}
