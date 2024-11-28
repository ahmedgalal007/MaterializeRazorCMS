using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AspnetCoreStarter.Entities;
using AspnetCoreStarter.Entities.Locals;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace AspnetCoreStarter.Entities
{
  public class User : MultiLangEntity<UserLocals,int>
  {
    // public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool IsVerified { get; set; }
    public string? ContactNumber { get; set; }

    // Role selection (Choose one role from the list)
    public string? SelectedRole { get; set; }

    // Exclude AvailablePlans from database modeling
    [NotMapped]
    public List<string>? AvailableRoles { get; set; }

    // Plan selection (Choose one plan from the list)
    public string? SelectedPlan { get; set; }

    // Exclude AvailablePlans from database modeling
    [NotMapped]
    public List<string>? AvailablePlans { get; set; }

    public override UserLocals AddLocal(UserLocals local)
    {
      Locales.Add(local);
      return local;
    }

    public override UserLocals RemoveLocal(UserLocals local)
    {
      Locales.Remove(local);
      return local;
    }

    public override UserLocals UpdateLocal(UserLocals local)
    {
      var current = Locales.Where(e => e.Id == local.Id).FirstOrDefault();
      if (current == null)
        return local;
        // return new KeyNotFoundException($"The localized version with id ({local.Id.ToString()}) not exsists");
      current.FirstName = local.FirstName;
      current.LastName = local.LastName;
      return current;
    }
  }
}
