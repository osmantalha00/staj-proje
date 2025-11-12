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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IdGenerator _idGenerator; 

        public TransactionsController(ITransactionService transactionService, IdGenerator idGenerator) 
        {
            _transactionService = transactionService;
            _idGenerator = idGenerator; 
        }

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> Get()
        {
            return await _transactionService.GetAllTransactionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if(transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionService.AddTransactionAsync(transaction, _idGenerator); 
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

       [HttpPut("{id}")]
public async Task<IActionResult> Put(int id, [FromBody] Transaction transaction)
{
    if (id != transaction.transaction_id)
    {
        return BadRequest(new { error = "ID mismatch." });
    }

    try
    {
        var existingTransaction = await _transactionService.GetTransactionByIdAsync(id);
        if (existingTransaction == null)
        {
            return NotFound(new { error = "Transaction not found" });
        }

       
        existingTransaction.account_id = transaction.account_id;
        existingTransaction.tarih = transaction.tarih;
        existingTransaction.tutar = transaction.tutar;
        existingTransaction.islem_turu = transaction.islem_turu;

        await _transactionService.UpdateTransactionAsync(existingTransaction);
        return NoContent();
    }
    catch (ValidationException e)
    {
        return BadRequest(new { error = e.Message });
    }
}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _transactionService.DeleteTransactionAsync(id);
                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}






/* 
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Transaction>>> Get()
        {
            return await _transactionService.GetAllTransactionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if(transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transaction transaction)
        {
            try
            {
                await _transactionService.AddTransactionAsync(transaction, new IdGenerator());
                return Ok();
            }
            catch (ValidationException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Transaction transaction)
        {
            if(id != transaction.transaction_id)
            {
                return BadRequest(new { error = "ID mismatch." });
            }
            try
            {
                await _transactionService.UpdateTransactionAsync(transaction);
                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _transactionService.DeleteTransactionAsync(id);
                return NoContent();
            }
            catch (ValidationException e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}


 


 
*/ 