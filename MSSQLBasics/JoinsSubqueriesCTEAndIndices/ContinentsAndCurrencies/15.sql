select k.ContinentCode,k.CurrencyCode,k.CurrencyUsage from
(select 
	c.ContinentCode,
	c.CurrencyCode,
	count(c.CurrencyCode) as CurrencyUsage,
	dense_rank() over (partition by c.ContinentCode order by count(c.CurrencyCode) desc) as CurrencyRank
from Countries as c
group by c.ContinentCode,c.CurrencyCode
having count(c.CurrencyCode) > 1) as k
where k.CurrencyRank = 1
order by k.ContinentCode,k.CurrencyUsage


