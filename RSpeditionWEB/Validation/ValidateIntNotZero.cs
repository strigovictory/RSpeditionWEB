namespace RSpeditionWEB.Validation
{
    internal class ValidateIntNotZero : ValidationAttribute
    {
        public string PropName { get; set; }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var stringVal = value.ToString();

                if(!string.IsNullOrEmpty(stringVal) && !string.IsNullOrWhiteSpace(stringVal))
                {

                    if (Int32.TryParse(stringVal, out int intVal))
                    {
                        if (intVal < 0 || intVal > 0)
							return true;
						else
						{
                            this.ErrorMessage = $"Поле {this.PropName ?? string.Empty} не должно быть равно нулю! ";
                            return false;
                        }
                    }
                    else
                    {
                        this.ErrorMessage = $"Заполните поле {this.PropName ?? string.Empty}! ";
                        return false;
                    }
                }
                else
                {
                    this.ErrorMessage = $"Заполните поле {this.PropName ?? string.Empty}! ";
                    return false;
                }
            }
            else
            {
                this.ErrorMessage = $"Заполните поле {this.PropName ?? string.Empty}! ";
                return false;
            }
        }
	}

	internal class ValidateDecimalNotZero : ValidationAttribute
	{
		public string PropName { get; set; }
		public override bool IsValid(object value)
		{
			if (value != null)
			{
				var stringVal = value.ToString();

				if (!string.IsNullOrEmpty(stringVal) && !string.IsNullOrWhiteSpace(stringVal))
				{

					if (decimal.TryParse(stringVal, out var decimal_val))
					{
						if (decimal_val < 0 || decimal_val > 0)
							return true;
						else
						{
							this.ErrorMessage = $"Поле {this.PropName ?? string.Empty} не должно быть равно нулю! ";
							return false;
						}
					}
					else
					{
						this.ErrorMessage = $"Заполните поле {this.PropName ?? string.Empty}! ";
						return false;
					}
				}
				else
				{
					this.ErrorMessage = $"Заполните поле {this.PropName ?? string.Empty}! ";
					return false;
				}
			}
			else
			{
				this.ErrorMessage = $"Заполните поле {this.PropName ?? string.Empty}! ";
				return false;
			}
		}
	}
}
