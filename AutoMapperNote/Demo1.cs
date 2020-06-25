using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AutoMapperNote
{
    internal class CalendarEvent
    {
        public DateTime EventDate { get; set; }
        public string Title { get; set; }
    }

    internal class CalendarEventSame
    {
        public DateTime EventDate { get; set; }
        public string Title { get; set; }
    }

    internal class CalendarEventForm
    {
        public DateTime EventDate { get; set; }
        public int EventHour { get; set; }
        public int EventMinute { get; set; }
        public string Title { get; set; }
    }

    public class Demo1
    {
        [Fact]
        public void 欄位一樣會自動處理()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                EventDate = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            var config = new MapperConfiguration(cfg =>
            {
                //欄位一樣 不須處理
                cfg.CreateMap<CalendarEvent, CalendarEventSame>();
            });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            CalendarEventSame act = mapper.Map<CalendarEvent, CalendarEventSame>(calendarEvent);

            Assert.Equal(new DateTime(2008, 12, 15, 20, 30, 0), act.EventDate);
            Assert.Equal("Company Holiday Party", act.Title);
        }

        [Fact]
        public void 部分欄位不一樣自動處理時會帶預設值()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                EventDate = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            var config = new MapperConfiguration(cfg =>
            {
                //欄位不同 就只會處理相同欄位名稱
                cfg.CreateMap<CalendarEvent, CalendarEventForm>();
            });

            var mapper = config.CreateMapper();
            CalendarEventForm act = mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

            Assert.Equal(new DateTime(2008, 12, 15, 20, 30, 0), act.EventDate);
            Assert.Equal(0, act.EventHour);
            Assert.Equal(0, act.EventMinute);
            Assert.Equal("Company Holiday Party", act.Title);
        }

        [Fact]
        public void 部分客製_其他名稱一樣會自己處理()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                EventDate = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            var config = new MapperConfiguration(cfg =>
            {
                //EventDate Title 因為名稱一樣 會自己處理
                //EventHour EventMinute 指定
                cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                   .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.EventDate.Hour))
                   .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.EventDate.Minute));
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            CalendarEventForm act = mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

            Assert.Equal(new DateTime(2008, 12, 15, 20, 30, 0), act.EventDate);
            Assert.Equal(20, act.EventHour);
            Assert.Equal(30, act.EventMinute);
            Assert.Equal("Company Holiday Party",act.Title);
        }

        [Fact]
        public void 可以設定忽略的欄位()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                EventDate = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            var config = new MapperConfiguration(cfg =>
            {
                //EventDate 因為名稱一樣 會自己處理
                //EventHour EventMinute 指定
                //Title 設定忽略
                cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                   .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.EventDate.Hour))
                   .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.EventDate.Minute))
                   .ForMember(dest => dest.Title, opt => opt.Ignore());
            });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            CalendarEventForm act = mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

            Assert.Equal(new DateTime(2008, 12, 15, 20, 30, 0), act.EventDate);
            Assert.Equal(20,act.EventHour);
            Assert.Equal(30,act.EventMinute);
            Assert.Null(act.Title);
        }


        [Fact]
        public void AssertConfigurationIsValid會驗證是不是有沒有缺少對應的設定()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                EventDate = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            var config = new MapperConfiguration(cfg =>
            {
                //欄位不同 就只會處理相同欄位名稱
                cfg.CreateMap<CalendarEvent, CalendarEventForm>();
            });

            //但是執行AssertConfigurationIsValid就會檢查有沒有 缺少對應的設定
            Exception ex = Assert.Throws<AutoMapperConfigurationException>(
                () => config.AssertConfigurationIsValid());
            Assert.StartsWith("\nUnmapped members were found", ex.Message);
        }

        [Fact]
        public void AssertConfigurationIsValid補上全部的設定()
        {
            // Model
            var calendarEvent = new CalendarEvent
            {
                EventDate = new DateTime(2008, 12, 15, 20, 30, 0),
                Title = "Company Holiday Party"
            };

            var config = new MapperConfiguration(cfg =>
            {
                //補上缺少的EventHour EventMinute
                cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                   .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.EventDate.Hour))
                   .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.EventDate.Minute));
            });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();
            CalendarEventForm act = mapper.Map<CalendarEvent, CalendarEventForm>(calendarEvent);

            Assert.Equal(20,act.EventHour);
            Assert.Equal(30,act.EventMinute);
        }

        [Fact]
        public void 有Exception時會嚴重影響效能()
        {
            // Model
            var config = new MapperConfiguration(cfg =>
            {
                //補上缺少的EventHour EventMinute
                cfg.CreateMap<CalendarEvent, CalendarEventForm>()
                   .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.EventDate.Hour))
                   .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.EventDate.Minute))
                   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => ToLower(src.Title)));
            });

            var calendarEventList = new List<CalendarEvent>();
            for (int i = 0; i < 500; i++)
            {
                calendarEventList.Add(
                    new CalendarEvent { EventDate = new DateTime(2008, 12, 15, 20, 30, 0), Title = "AA" });
            }

            var mapper = config.CreateMapper();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var act = mapper.Map<List<CalendarEventForm>>(calendarEventList);
            watch.Stop();

            var calendarEventList2 = new List<CalendarEvent>();
            for (int i = 0; i < 500; i++)
            {
                calendarEventList2.Add(
                    new CalendarEvent { EventDate = new DateTime(2008, 12, 15, 20, 30, 0), Title = null });
            }
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            var act2 = mapper.Map<List<CalendarEventForm>>(calendarEventList2);
            watch2.Stop();
            //                      9.3秒          > 0.12秒
            Assert.True(watch2.ElapsedMilliseconds > watch.ElapsedMilliseconds);
     
        
        }

        private string ToLower(string value)
        {
            return value.ToLower();
        }
    }
}
