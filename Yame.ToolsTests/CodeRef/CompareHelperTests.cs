using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.CodeRef;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Yame.Tools.NetCore.CodeRef;

namespace Yame.Tools.CodeRef.Tests
{
    [TestClass()]
    public class CompareHelperTests
    {
        [TestMethod()]
        public void CompareListTest()
        {
            //Arrange
            var originalList = new List<HotfixInfo>
            {
                new HotfixInfo{ Id=1, Hotfix="KB111001", FullTitle="更新啟動問題"},
                new HotfixInfo{ Id=2, Hotfix="KB111002", FullTitle="更新關機問題"},
                new HotfixInfo{ Id=3, Hotfix="KB111003", FullTitle="更新卡頓問題"},
                new HotfixInfo{ Id=4, Hotfix="KB111004", FullTitle="優化驅動程式當機"},
            };


            var nowList = new List<HotfixInfo>
            {
                new HotfixInfo { Hotfix = "KB111001", FullTitle = "更新啟動問題" },
                new HotfixInfo { Hotfix = "KB111003", FullTitle = "更新切換螢幕時卡頓問題" },
                new HotfixInfo { Hotfix = "KB111006", FullTitle = "安全性更新" },
                new HotfixInfo { Hotfix = "KB111007", FullTitle = "穩定度更新" },
            };

            var expected = new CompareResult();
            expected.ToBeAdded = new List<HotfixInfo>
            {
                new HotfixInfo { Hotfix = "KB111006", FullTitle = "安全性更新" },
                new HotfixInfo { Hotfix = "KB111007", FullTitle = "穩定度更新" },
            };
            expected.ToBeUpdated = new List<HotfixInfo>
            {
                  new HotfixInfo { Hotfix = "KB111003", FullTitle = "更新切換螢幕時卡頓問題" },
            };
            expected.ToBeDeleted = new List<HotfixInfo>
            {
                  new HotfixInfo{ Id=2, Hotfix="KB111002", FullTitle="更新關機問題"},
                  new HotfixInfo{ Id=4, Hotfix="KB111004", FullTitle="優化驅動程式當機"},
            };
            //Act
            var actual = CompareHelper.CompareList(nowList, originalList);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}