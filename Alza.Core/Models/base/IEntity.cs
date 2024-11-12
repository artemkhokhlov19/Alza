using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Core.Models;

public interface IEntity<T>
{
    new T Id { get; set; }
}
