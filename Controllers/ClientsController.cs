#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DelieveryWebApplication;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;

namespace DelieveryWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class ClientsController : Controller
    {
        private readonly delieveryContext _context;

        public ClientsController(delieveryContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,Name,Surname,Street,House,Flat,PhoneNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,Surname,Street,House,Flat,PhoneNumber")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        //перегляд усіх листів (в даному випадку категорій)
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {         
                                    foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                    {
                                        try
                                        {
                                            Client client = new Client();
                                            client.Name = row.Cell(1).Value.ToString();
                                            client.Surname = row.Cell(2).Value.ToString();
                                            client.Street = row.Cell(3).Value.ToString();
                                            client.House = Convert.ToInt32(row.Cell(4).Value);
                                            client.Flat = Convert.ToInt32(row.Cell(5).Value);
                                            client.PhoneNumber = row.Cell(6).Value.ToString();

                                            var phonesInDB = (from cl in _context.Clients
                                                              select cl.Flat);
                                            //у разі наявності клієнта додавати його непотрібно, індентифікація по номеру квартири
                                            if (!(phonesInDB.Contains(client.Flat)))
                                                _context.Clients.Add(client);
                                        }
                                        catch (Exception e)
                                        {
                                            //logging самостійно :)

                                        }
                                    }
                                }
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
        

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var clients = _context.Clients.ToList();
                //тут, для прикладу ми пишемо усі книжки з БД, в своїх проєктах ТАК НЕ РОБИТИ (писати лише вибрані)
                foreach (var per in clients)
                {
                    var worksheet = workbook.Worksheets.Add(per.PhoneNumber);

                    worksheet.Cell("A1").Value = "Назва";
                    worksheet.Cell("B1").Value = "Ім`я";
                    worksheet.Cell("C1").Value = "Прізвище";
                    worksheet.Cell("D1").Value = "Вулиця";
                    worksheet.Cell("E1").Value = "Будинок";
                    worksheet.Cell("F1").Value = "Квартира";
                    worksheet.Cell("G1").Value = "Номер телефону";
                    worksheet.Row(1).Style.Font.Bold = true;


                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for (int i = 0; i < clients.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = clients[i].Name;
                        worksheet.Cell(i + 2, 2).Value = clients[i].Surname;
                        worksheet.Cell(i + 2, 3).Value = clients[i].Street;
                        worksheet.Cell(i + 2, 4).Value = clients[i].House;
                        worksheet.Cell(i + 2, 5).Value = clients[i].Flat;
                        worksheet.Cell(i + 2, 7).Value = clients[i].PhoneNumber;

                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        //змініть назву файла відповідно до тематики Вашого проєкту

                        FileDownloadName = $"clients_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }



    }
}
