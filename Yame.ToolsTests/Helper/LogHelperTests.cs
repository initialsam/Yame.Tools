using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yame.Tools.NetCore.Helper;
using Yame.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace Yame.Tools.NetCore.Helper.Tests
{
    class TestA
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldType FieldType { get; set; }
        public int RoleGroupId { get; set; }
    }
    enum FieldType
    {
        None = 0,
        Speed = 1,
        Strong = 2
    }
    [TestClass()]
    public class LogHelperTests
    {
        [TestMethod()]
        public void DetailedCompareTest()
        {
            var modeOrig = new TestA() { Id = 1, Name = "BB" };
            var modeEdit = modeOrig.DeepCopy();
            modeEdit.Id = 2;
            modeEdit.Name = "xyz";

            var actual = modeOrig.DetailedCompare(modeEdit);
            actual.Should().BeEquivalentTo("1 ➜ 2<BR/>BB ➜ xyz");

            var actual2 = modeOrig.DetailedCompare(modeEdit, nameof(TestA.Id));
            actual2.Should().BeEquivalentTo("BB ➜ xyz");
        }

        [TestMethod()]
        public void DetailedCompareWithTitleTest()
        {
            var modeOrig = new TestA() { Id = 1, Name = "BB" };
            var modeEdit = modeOrig.DeepCopy();
            modeEdit.Id = 2;
            modeEdit.Name = "xyz";
            var master = "Title";

            var actual = modeOrig.DetailedCompareWithTitle(modeEdit, master);
            actual.Should().BeEquivalentTo("Title<BR/>1 ➜ 2<BR/>BB ➜ xyz");

            var actual1 = modeOrig.DetailedCompareWithTitle(modeEdit, master, nameof(TestA.Name));
            actual1.Should().BeEquivalentTo("Title<BR/>1 ➜ 2");
        }

        [TestMethod()]
        public void DetailedCompareWithSpecialTextTest1()
        {
            var modeOrig = new TestA() { Id = 8, Name = "BB", FieldType = FieldType.Speed , RoleGroupId = 1 };
            var modeEdit = modeOrig.DeepCopy();
            modeEdit.Id = 9;
            modeEdit.Name = "xyz";
            modeEdit.FieldType = FieldType.Strong;
            modeEdit.RoleGroupId = 2;
            var actual = modeOrig.DetailedCompare(modeEdit);
            actual.Should().BeEquivalentTo("8 ➜ 9<BR/>BB ➜ xyz<BR/>Speed ➜ Strong<BR/>1 ➜ 2");

            var RoleGroupList = new Dictionary<int, string> { { 1, "AGroup" }, { 2, "BGroup" } };
            Dictionary<string, Func<object, string>> specialText = new Dictionary<string, Func<object, string>>();
            specialText.Add(nameof(TestA.Name), x => $"{x}名稱");
            specialText.Add(nameof(TestA.RoleGroupId), x => RoleGroupList[Convert.ToInt32(x)]);

            var actual2 = modeOrig.DetailedCompare(modeEdit, specialText, nameof(TestA.Id), nameof(TestA.FieldType));
            actual2.Should().BeEquivalentTo("BB名稱 ➜ xyz名稱<BR/>AGroup ➜ BGroup");
        }
    }
}