using HMS_Team1_PRN211_SE1608.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace HMS_Team1_PRN211_SE1608.Logic
{
    public class RoomManager
    {
        public List<Room> AvancedSearchByRoomAndPaging(string name, DateTime? fromdate, DateTime? todate, int? size, int? price, int? sort, int offset, int pagesize)
        {
            List<Room> list = new List<Room>();
            var context = new PRN211_HMSContext();


            list = (from r in context.Rooms.ToList<Room>()
                    join s in context.SizeRooms.ToList<SizeRoom>() on r.SizeId equals s.SizeId
                    join reser in context.Reservations.ToList<Reservation>() on r.RoomId equals reser.RoomId into lf
                    from subre in lf.DefaultIfEmpty()
                    where r.RoomName.Contains(name ?? r.RoomName)
                    && s.SizeId == (size ?? s.SizeId)
                    && s.Price == (price ?? s.Price)

                    select r).ToList();

            List<Room> ex = new List<Room>();
            ex = (from r in context.Rooms.ToList<Room>()
                  join s in context.SizeRooms.ToList<SizeRoom>() on r.SizeId equals s.SizeId
                  join reser in context.Reservations.ToList<Reservation>() on r.RoomId equals reser.RoomId
                  where reser.StartDate >= (fromdate ?? reser.StartDate) && reser.EndDate <= (todate ?? reser.EndDate)
                  select r).ToList();


            if (fromdate != null || todate != null)
            {
                list = list.Except(ex).ToList();


            }

            if (sort == 1) list = list.OrderBy(y => y.RoomName).ToList();
            else if (sort == 2) list = list.OrderByDescending(y => y.RoomName).ToList();
            else if (sort == 3) list = list.OrderBy(y => y.Size.Price).ToList();
            else if (sort == 4) list = list.OrderByDescending(y => y.Size.Price).ToList();
            else list = list.OrderBy(y => y.Size.Price).ToList();
            list = list.Skip(offset - 1).Take(pagesize).ToList();



            // đã lấy ra bản ghi phù hợp với trang đó :
            return list;
        }
        public int countRoom(string name, DateTime? fromdate, DateTime? todate, int? size, int? price)
        {
            List<Room> list = new List<Room>();
            using (var context = new PRN211_HMSContext())
            {
                list = (from r in context.Rooms.ToList<Room>()
                        join s in context.SizeRooms.ToList<SizeRoom>() on r.SizeId equals s.SizeId
                        join reser in context.Reservations.ToList<Reservation>() on r.RoomId equals reser.RoomId into lf
                        from subre in lf.DefaultIfEmpty()
                        where r.RoomName.Contains(name ?? r.RoomName)
                        && s.SizeId == (size ?? s.SizeId)
                        && s.Price == (price ?? s.Price)

                        select r).ToList();


                List<Room> ex = new List<Room>();
                ex = (from r in context.Rooms.ToList<Room>()
                      join s in context.SizeRooms.ToList<SizeRoom>() on r.SizeId equals s.SizeId
                      join reser in context.Reservations.ToList<Reservation>() on r.RoomId equals reser.RoomId
                      where reser.StartDate >= (fromdate ?? reser.StartDate) && reser.EndDate <= (todate ?? reser.EndDate)
                      select r).ToList();


                if (fromdate != null || todate != null)
                {
                    list = list.Except(ex).ToList();



                }



            }



            // đã lấy ra bản ghi phù hợp với trang đó :
            return list.Count;
        }



    }

}
