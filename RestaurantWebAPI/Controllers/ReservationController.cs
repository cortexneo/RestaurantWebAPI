using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(AppDbContext context, ILogger<ReservationController> logger = null)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll()
        {
            return Ok(_context.Set<Reservation>().Include(x => x.MenuItem));
        }

        [HttpGet("GetReservationById")]
        public ActionResult<Reservation> GetSpecificMenuItem(int id)
        {
            return Ok(_context.Set<Reservation>().Where(x => x.Id == id).Include(x => x.MenuItem));
        }

        [HttpPost]
        public ActionResult Create(string name, int menuItemId)
        {
            try
            {
                var reservation = new Reservation
                {
                    Name = name,
                    Date = DateTime.UtcNow,
                    MenuItemId = menuItemId
                };

                _context.Set<Reservation>().Add(reservation);
                _context.SaveChanges();

                return Ok(reservation);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult Update(int id, string name, int menuItemId)
        {
            try
            {
                var itemToUpdate = _context.Set<Reservation>().Find(id);

                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = name;
                    itemToUpdate.Date = DateTime.UtcNow;
                    itemToUpdate.MenuItemId = menuItemId;

                    _context.Entry<Reservation>(itemToUpdate)
                        .CurrentValues
                        .SetValues(itemToUpdate);

                    _context.SaveChanges();

                    return Ok(itemToUpdate);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var menuItemToRemove = _context.Set<Reservation>().Find(id);

                if (menuItemToRemove != null)
                {
                    _context.Set<Reservation>().Remove(menuItemToRemove);
                    _context.SaveChanges();

                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
