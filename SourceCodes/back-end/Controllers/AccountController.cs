using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project;
using Project.Services;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IdGenerator _idGenerator;

        public AccountsController(IAccountService accountService, IdGenerator idGenerator)
        {
            _accountService = accountService;
            _idGenerator = idGenerator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return await _accountService.GetAllAccountsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if(account == null)
            {
                return NotFound();
            }
            return account;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account account)
        {
            try
            {
                await _accountService.AddAccountAsync(account, _idGenerator);
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

      [HttpPut("{id}")]
public async Task<IActionResult> Put(int id, [FromBody] Account account)
{
    if(id != account.account_id)
    {
        return BadRequest();
    }

    try
    {
        var existingAccount = await _accountService.GetAccountByIdAsync(id);
        if (existingAccount == null)
        {
            return NotFound(new { error = "Account not found" });
        }

        // existingAccount'un _id değerini koruyarak diğer değerleri güncelleyme yaptık
        existingAccount.customer_id = account.customer_id;
        existingAccount.hesap_turu = account.hesap_turu;
        existingAccount.acilis_tarihi = account.acilis_tarihi;
        existingAccount.bakiye = account.bakiye;
        
        await _accountService.UpdateAccountAsync(existingAccount);

        return NoContent();
    }
    catch (ValidationException ex)
    {
        return BadRequest(new { error = ex.Message });
    }
}


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.DeleteAccountAsync(id);
            return NoContent();
        }
    }
}
