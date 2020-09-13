/*
public static gm.Recompense ToRecompense(this IDataRecord ent)
        {
            if (ent == null) return null;
            return new gm.Recompense
            {

                id = (int)ent[nameof(gm.Recompense.id)],
                nom = (string)ent[nameof(gm.Recompense.nom)],
                description = ent[nameof(gm.Recompense.description)].CString(),
                actif = (int)ent[nameof(gm.Recompense.actif)]
            };
        }
*/
use Genealogie
--select * from v_tables

declare @yes nvarchar(max);
declare @ttype varchar(100);

declare @nomtable varchar(250) = 'Utilisateur'
DECLARE @idcol int
declare @nomcol VARCHAR(250)
declare @typecol VARCHAR(25)
declare @longueurmax int
declare @precision int
declare @scale int
declare @estnullable int

declare cc cursor for 
select distinct nomtable from v_tables
where nomtable = 'utilisateurrole'
;


 

 open cc 
 fetch cc into @nomtable
 while @@FETCH_STATUS = 0
 begin

	--print @nomtable

	print concat('public static',' ',@nomtable,' ','Vers',@nomtable,'(this IDataRecord idr) {')
    print 'if (idr == null) return null;'
	print concat('return new',' ',@nomtable,' ','{')

	DECLARE c CURSOR FOR
    SELECT  idcol, nomcol, typecol, longueurmax, precision, scale, estnullable FROM v_tables
	where nomtable = @nomtable
	order by idcol
	OPEN c
 
	FETCH c INTO @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
 

	set @yes = '';

	WHILE @@FETCH_STATUS = 0
	BEGIN


	    select @ttype = dbo.DonnerTypeCSharp(@typecol,@estnullable);
		

		if @yes <> ''
			set @yes = CONCAT(@yes,char(10),',');
		
		

		set @yes = concat(@yes,@nomcol,'=(',@ttype,')idr[nameof(',@nomtable,'.',@nomcol,')]','/*',@typecol,'*/')

		FETCH c INTO  @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
	
	END
 
	CLOSE c
	DEALLOCATE c
	print @yes
	print '};}'
	fetch cc into @nomtable

end
close cc
deallocate cc






GO
	

GO



