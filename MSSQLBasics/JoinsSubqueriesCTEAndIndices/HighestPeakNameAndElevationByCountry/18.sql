select top(5) k.CountryName,
	isnull(k.PeakName,'(no highest peak)') as [Highest Peak Name],
	isnull(k.MaxElevation,0) as [Highest Peak Elevation],
	isnull(k.MountainRange,'(no mountain)') as [Mountain]
	--case
	--  when k.PeakName is null
	--  then '(no highest peak)'
	--  else k.PeakName
	--end as [Highest Peak Name],
	--case
	--  when k.MaxElevation is null
	--  then '0'
	--  else k.MaxElevation
	--end as [Highest Peak Elevation],
	--case 
	--  when k.MountainRange is null
	--  then '(no mountain)'
	--  else k.MountainRange
	--end as [Mountain]
 from
(select 
	c.CountryName,
	p.PeakName,
	max(p.Elevation) as MaxElevation,
	m.MountainRange,
	dense_rank() over (partition by c.CountryName order by max(p.Elevation) desc) as ElevationRank
from Countries as c
left join MountainsCountries as mc on c.CountryCode = mc.CountryCode
left join Mountains as m on mc.MountainId = m.Id
left join Peaks as p on m.Id = p.MountainId
group by c.CountryName,p.PeakName,m.MountainRange) as k
where k.ElevationRank = 1
order by k.CountryName,k.PeakName
