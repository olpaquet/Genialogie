/*
drop database Genealogie

go
*/
/*
create database Genealogie
go
*/
use Genealogie
go
/****DROPFK*/

alter table UtilisateurAbonnement drop constraint fk_utilisateurabonnement_abonne 
alter table UtilisateurAbonnement drop constraint fk_utilisateurabonnement_abonnement 
alter table Utilisateurrole drop constraint fk_utilisateurrole_role
alter table Utilisateurrole drop constraint fk_utilisateurrole_utilisateur
alter table Arbre drop constraint fk_arbre_bloqueur
alter table Arbre drop constraint fk_arbre_createur
alter table Arbre drop constraint fk_arbre_blocage
alter table couple drop constraint fk_couple_personne 
alter table couple drop constraint fk_couple_partenaire
alter table Personne drop constraint fk_personne_pere
alter table Personne drop constraint fk_personne_mere
/*alter table UtilisateurNouvelle drop constraint fk_utilisateurnouvelle_publicateur
alter table UtilisateurNouvelle drop constraint fk_utilisateurnouvelle_nouvelle*/
alter table Nouvelle drop constraint fk_nouvelle_createur 
alter table MessageForum drop constraint fk_messageforum_theme
alter table MessageForum drop constraint fk_messageforum_publicateur

alter table MessageLu drop constraint fk_messagelu_message
 alter table MessageLu drop constraint fk_messagelu_lecteur

 alter table MessageEfface drop constraint fk_messageefface_message
 alter table MessageEfface drop constraint fk_messageefface_effaceur

alter table Message drop constraint fk_message_emetteur
 alter table Message drop constraint fk_message_conversation

/****ENDDROPFK*/

/****SUPERVUE*/

drop view v_tables
go
CREATE view v_tables as
select 
--distinct type_desc
o.name nomtable, 
c.column_id idcol,
c.name nomcol,
t.name typecol,
c.max_length longueurmax, c.precision, c.scale, c.is_nullable estnullable
from sys.all_objects o join sys.all_columns c on o.object_id = c.object_id join sys.types t on c.user_type_id = t.user_type_id
where o.type_desc = 'USER_TABLE'
and o.schema_id = 1
--order by o.name, c.column_id

GO



/*****DROP*/
drop table Utilisateur
drop table Abonnement
drop table Role
drop table Arbre
drop table Blocage
drop table Nouvelle
drop table Personne
drop table Theme
drop table Conversation
drop table Message
drop table Couple
drop table MessageLu
drop table MessageEfface
drop table UtilisateurAbonnement

drop table UtilisateurRole

/*drop table UtilisateurNouvelle*/

drop table MessageForum



go

/***DROP*/



/***TABLES*/


 create table Utilisateur
 (
 id int identity (1,1) not null,
 login nvarchar(50) not null,
 nom nvarchar(50) not null,
 prenom nvarchar(50)  null,
 email nvarchar(200) not null,
 datedenaissance datetime,
 homme int not null,
 cartedepayement nvarchar(50),
 motdepasse varbinary(64) not null,
 presel nvarchar(255) not null,
 postsel nvarchar(255) not null,
 actif int not null default 1
 constraint pk_utilisateur primary key (id)
 );
 go
 --comment on column utilisateur.id is 'Id';
 go

 
 create unique index iu_utilisateur_login on Utilisateur (login)

 go


 create table Abonnement(
 id int identity(1,1) not null,
 nom nvarchar(50) not null,
 description nvarchar(1000) not null,
 duree int not null,
 prix decimal not null default 0.0,
 nombremaxarbres int not null default 0,
 nombremaxpersonnes int not null default 0,
 actif int not null default 1
 constraint pk_abonnement primary key (id)
 )
 go
 create unique index iu_abonnement on Abonnement(id,nom)
 go

 create table UtilisateurAbonnement(
 idabonne int not null,
 idabonnement int not null,
 dateabonnement datetime not null,
 cartedepayement nvarchar(50) not null
 constraint pk_utilisateurabonnement primary key(idabonne,idabonnement, dateabonnement)
 )
 
 create table Role(
 id int identity(1,1) not null,
 nom nvarchar(50) not null,
 description nvarchar(1000) not null,
 actif int not null default 1
 constraint pk_role primary key(id)
 )
 create unique index iu_role on Role(nom)

 go
 create table UtilisateurRole(
 idutilisateur int not null,
 idrole int not null
 constraint pk_utilisateurrole primary key(idutilisateur,idrole)
 )
 
 go

 create table Arbre(
 id int identity(1,1) not null,
 nom nvarchar(50) not null,
 description nvarchar(1000) not null,
 idcreateur int not null,
 datecreation datetime not null,
 idblocage int,
 idbloqueur int,
 dateblocage datetime
 constraint pk_arbre primary key(id)
 )
 create unique index iu_arbre_nom on Arbre(nom,idcreateur)
 
 go
 
 
 create table Blocage(
 id int identity(1,1),
 nom nvarchar(50) not null,
 description nvarchar(50) not null,
 actif int not null default 1
 constraint pk_blocage primary key(id)
 )
 create unique index iu_blocage_nom on Blocage(nom)

 go
 
 create table Personne(
 id int identity (1,1) not null,
 nom nvarchar(50) not null,
 prenom nvarchar(50),
 datedenaissance datetime,
 datededeces datetime,
 idarbre int not null,
 dateajout datetime not null,
 idpere int,
 idmere int
 constraint pk_personne primary key (id)
 )


 create table Couple(
 idpersonne int not null,
 idpartenaire int not null,
 datedebut datetime not null,
 datefin datetime
 constraint pk_couple primary key (idpersonne, idpartenaire, datedebut)
 )
 
 create table Nouvelle(
 id int identity(1,1) not null,
 titre nvarchar(50) not null,
 description nvarchar(1000) not null,
 idcreateur int not null,
 actif int not null default 1
 constraint pk_nouvelle primary key(id)
 )

 

 create table Theme(
 id int identity(1,1) not null,
 titre nvarchar(50) not null,
 description nvarchar(1000) not null,
 actif int not null default 1
 constraint pk_theme primary key(id)
 )
 create unique index iu_theme_titre on Theme(titre)

 create Table Conversation(
 id int identity(1,1) not null,
 date datetime not null
 constraint pk_conversation primary key (id)
 )

 create table Message(
 id int identity(1,1) not null,
 sujet nvarchar(50) not null,
 texte nvarchar(max) not null,
 idemetteur int not null,
 idconversation int not null
 constraint pk_message primary key (id)
 )
 
 /*
 create table UtilisateurNouvelle(
 idpublicateur int not null,
 idnouvelle int not null,
 datepublication datetime not null
 constraint pk_utilisateurnouvelle primary key(idpublicateur,idnouvelle)
 )*/
 
 create table MessageForum(
 id int identity(1,1) not null,
 sujet nvarchar(50) not null,
 texte nvarchar(max),
 idtheme int not null,
 idpublicateur int not null,
 datepublication datetime,
 actif int not null default 1
 constraint pk_messageforum primary key(id)
 )

 create table MessageLu(
 idmessage int not null,
 idlecteur int not null,
 date datetime
 constraint pk_messagelu primary key (idmessage,idlecteur)
 )
 create table MessageEfface(
 idmessage int not null,
 ideffaceur int not null,
 date datetime
 constraint pk_messageefface primary key (idmessage,ideffaceur)
 )

 /*****ENDTABLES*/


 /*****FOREIGNKEYS*/
 alter table UtilisateurAbonnement add constraint fk_utilisateurabonnement_abonne foreign key (idabonne) references utilisateur(id)
 alter table UtilisateurAbonnement add constraint fk_utilisateurabonnement_abonnement foreign key (idabonnement) references utilisateur(id)
 alter table Utilisateurrole add constraint fk_utilisateurrole_utilisateur foreign key(idutilisateur) references utilisateur(id)
 alter table Utilisateurrole add constraint fk_utilisateurrole_role foreign key(idrole) references Role(id)
  

 
 alter table Arbre add constraint fk_arbre_createur foreign key (idcreateur) references Utilisateur(id)
 alter table Arbre add constraint fk_arbre_bloqueur foreign key (idbloqueur) references Utilisateur(id)
 alter table Arbre add constraint fk_arbre_blocage foreign key(idblocage) references Blocage(id)


 alter table couple add constraint fk_couple_personne foreign key (idpersonne) references Personne(id)
 alter table couple add constraint fk_couple_partenaire foreign key (idpartenaire) references Personne(id)

 alter table Personne add constraint fk_personne_pere foreign key (idpere) references Personne(id)
 alter table Personne add constraint fk_personne_mere foreign key (idmere) references Personne(id)
 /*alter table UtilisateurNouvelle add constraint fk_utilisateurnouvelle_publicateur foreign key(idpublicateur) references Utilisateur(id)
 alter table UtilisateurNouvelle add constraint fk_utilisateurnouvelle_nouvelle foreign key(idnouvelle) references Nouvelle(id)*/

 alter table Nouvelle add constraint fk_nouvelle_createur foreign key (idcreateur) references Utilisateur(id)

 alter table MessageForum add constraint fk_messageforum_theme foreign key (idtheme) references Theme(id)
 alter table MessageForum add constraint fk_messageforum_publicateur foreign key (idpublicateur) references Utilisateur(id)

 alter table MessageLu add constraint fk_messagelu_message foreign key (idmessage) references Message(id)
 alter table MessageLu add constraint fk_messagelu_lecteur foreign key (idlecteur) references Utilisateur(id)

 alter table MessageEfface add constraint fk_messageefface_message foreign key (idmessage) references Message(id)
 alter table MessageEfface add constraint fk_messageefface_effaceur foreign key (ideffaceur) references Utilisateur(id)

 alter table Message add constraint fk_message_emetteur foreign key (idemetteur) references Utilisateur(id)
 alter table Message add constraint fk_message_conversation foreign key (idconversation) references Conversation(id)

 /*****ENDFOREIGNKEYS*/

/*****fonctionnalités*/
go
drop function nbentrees
go
create  function nbentrees (@st nvarchar(max), @sep nvarchar(1))
returns int
as
begin
declare @pos int = 0

declare @ret int = 0;
if @st is null
	return @ret
set @pos = charindex(@sep,@sep, @pos + 1)
while @pos > 0

	begin
	set @ret = @ret + 1
	set @pos = charindex(@sep,@st, @pos + 1)
	end


return @ret
end

go
drop function entree 
go
create function entree(@st nvarchar(max), @po int, @sep nvarchar(1) = ",")
returns nvarchar(max)
as
begin

	declare @ret nvarchar(max)

	declare @nb int = (select dbo.nbentrees(@st, @sep))
	if @nb = 1
		return @st

	/*if @nb > @po or @po <= 1 or @st = null
		RAISERROR ()*/
	

	declare @posmem int = 0
	

	declare @i int = 0
	if @st is null or @po = 0
		return null

	while @i <> @po and @i < @nb
	begin
	
		declare @pos int = charindex( @sep, @st,@posmem + 1);
		set @i = @i + 1
		
		
		
		
		if @i = @po
		begin
			if @pos = @posmem
				return ''
			if @pos <= @posmem
				return substring(@st, @posmem+1,len(@st) - (@posmem + 1) + 1)
				
			else
				return substring(@st, @posmem + 1, @pos - @posmem - 1)
				
			
			
		end
			
		set @posmem = @pos	


	end

	return null
	
end

go
drop function PositionDans

go

create function PositionDans (@st nvarchar(max), @cherche nvarchar(max), @sep nvarchar(1) = ',')
returns int
as
begin
if @st is null 
	return 0

declare @max int = (select dbo.nbentrees(@st, @sep))

declare @i int = 1

while @i <= @max
begin
	if @cherche = (select dbo.entree(@st, @i, @sep))
		return @i
	set @i = @i + 1
end

return 0
end
go

drop function ConstruireHMotdepasse
go

CREATE FUNCTION ConstruireHMotdepasse 
(@motdepasse nVARCHAR(50), @presel nVARCHAR(255), @postsel nvarchar(255))
RETURNS VARBINARY(64)
AS
BEGIN
    RETURN HASHBYTES('SHA2_512',CONCAT(@presel,@motdepasse,@postsel))
END
GO

 drop function ControlerUtilisateur
 

 go

 CREATE FUNCTION ControlerUtilisateur 
(@login nvarchar(50), @motdepasse nVARCHAR(50), @option nvarchar(max) = null)
RETURNS int 
AS
BEGIN

	/*declare @presel nvarchar(255);
	declare @postsel nvarchar(255);*/
	--declare @epw varbinary(64);
	declare @u int;	


	select @u = count(*)
	from utilisateur 
	where login = @login and 
	dbo.ConstruireHMotdepasse(@motdepasse, presel, postsel) = motdepasse;
	/*if @u = null	
		return 0*/
	return @u;    
END
go
drop procedure changersel
go
create procedure changersel
@reponse int out,
@login nvarchar(50),
@motdepasse nvarchar(50),
@presel nvarchar(255),
@postsel nvarchar(255)
as
begin
declare @pw varbinary(64)
select @reponse = dbo.ControlerUtilisateur(@login,@motdepasse,null);
if @reponse = 0
	return @reponse;
select @pw = dbo.ConstruireHMotdepasse(@motdepasse, @presel,@postsel)
update utilisateur set motdepasse = @pw, presel = @presel, postsel = @postsel where login = @login
return 1

end
GO
drop procedure pchangermotdepasse
go

create procedure pchangermotdepasse
 @xreponse  int out,
@xlogin nvarchar(50), @vieuxmotdepasse nvarchar(50), 
@motdepasse nvarchar(50), @xoption nvarchar(max)
as
begin
	declare @presel nvarchar(255);
	declare @postsel nvarchar(255);
	declare @epw varbinary(64);
	declare @u int;
	declare @id int;
	
	select @id = id, @presel = presel, @postsel=postsel 
	from utilisateur 
	where login = @xlogin 
	and dbo.ConstruireHMotdepasse(@vieuxmotdepasse,presel,postsel) = motdepasse;
	
	if @id is null
		set @xreponse = 0;
	else 
	begin
	    set @epw = dbo.ConstruireHMotdepasse(@motdepasse,@presel,@postsel);
		update utilisateur set motdepasse = @epw where id = @id;
		set @xreponse = 1;
	end
    
end
go



/***********CRUD*/
go	
drop procedure utilisateurrole_cre
go
create PROCEDURE utilisateurrole_cre
 @idutilisateur int , @idrole int
AS
begin try
insert into utilisateurrole (idutilisateur,idrole) values (@idutilisateur,@idrole);
end try
begin catch
if @@error <> 2167
	THROW
end catch

go
drop procedure utilisateurrole_mod
go
create PROCEDURE utilisateurrole_mod
@idutilisateur int,@idrole int
AS
/*update utilisateurrole
set idrole=@idrole
where idutilisateur=@idutilisateur

;*/
go
drop procedure utilisateurrole_eff
go
create procedure utilisateurrole_eff
@idutilisateur int,@idrole int
AS
delete utilisateurrole 
where idutilisateur=@idutilisateur and idrole=@idrole
;

go
drop procedure utilisateur_cre
go

create PROCEDURE utilisateur_cre
 @id int out, @login nvarchar(100), @nom nvarchar(100), @prenom nvarchar(100), 
 @email nvarchar(400), @datedenaissance datetime, @homme int, @cartedepayement nvarchar(50), 
 @motdepasse nvarchar(50), @presel nvarchar(510), @postsel nvarchar(510),
 @roles nvarchar(max) = null
AS
begin
	begin try
		begin transaction
		declare @mp varbinary(64);
		set @mp = dbo.ConstruireHMotdepasse(@motdepasse,@presel,@postsel);
		insert into utilisateur (login,nom,prenom,email,datedenaissance,homme,cartedepayement,motdepasse,presel,postsel) values (@login,@nom,@prenom,@email,@datedenaissance,@homme,@cartedepayement,@mp,@presel,@postsel);
		--save transaction ut

		--begin transaction ut
		--declare @i int = 1/0
		set @id = @@IDENTITY
		declare @nb int 
		select @nb = (select dbo.nbentrees( @roles, ','))
		declare @compteur int = 1
		declare @jj int = 0

		--exec utilisateurrole_cre @id, 1
		while @compteur <= @nb
		
		begin
			set @jj = (select dbo.entree(@roles, @compteur, ','))
			--set @jj = 1;

			
			exec utilisateurrole_cre @id, @jj
			set @compteur = @compteur + 1
		end
		
		commit
	end try
	begin catch

	--[10:44] Michaël Person
    

declare @ErrorMessage nvarchar(max),@ErrorSeverity int,@ErrorState int;
select @ErrorMessage = ERROR_MESSAGE()+' Line '+ cast(ERROR_LINE() as nvarchar(5))
,@ErrorSeverity = ERROR_SEVERITY(),
@ErrorState = ERROR_STATE();
--if @@trancount >0rollbacktransaction;
set @id = 0
rollback
print concat('coucou=>',@ErrorMessage)
raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState);




		--rollback transaction ut
		
		declare @flute nvarchar(max)
		select @flute = @@ERROR
		--throw
		--raiserror(@flute)

		raiserror('flute',1,1)
		--raiserror(error_message(),1,1)
		set @id = 0
	end catch
end
go
drop procedure utilisateur_mod
go
create PROCEDURE utilisateur_mod
@id int,@nom nvarchar(100),@prenom nvarchar(100),
@email nvarchar(400),@datedenaissance datetime,@homme int, @cartedepayement nvarchar(50),
@roles nvarchar(max) 
AS
begin
begin try
begin transaction

update utilisateur
set nom=@nom,prenom=@prenom,email=@email,datedenaissance=@datedenaissance,homme=@homme, 
cartedepayement=@cartedepayement
where id=@id


/* effacer mauvais rôles*/
declare @idrole int


begin try
close c
deallocate c
end try
begin catch
end catch


declare c cursor for select idrole from utilisateurrole where idutilisateur = @id

open c
fetch c into @idrole

declare @dedans int
declare @position int

WHILE @@FETCH_STATUS = 0
BEGIN
	
	set @position = (select dbo.PositionDans(@roles, @idrole,','))
	
	if @position = 0 or @roles is null
		begin
		exec utilisateurrole_eff @id, @idrole
		end
	fetch c into @idrole
end
close c
deallocate c
/* ajouter rôles*/
declare @i int = 1
declare @nb int 
declare @xx int
set @nb = (select dbo.nbentrees(@roles, ','))
declare @role int
declare @xxx nvarchar(max)
while @i <= @nb
begin
	set @xxx = (select dbo.entree(@roles, @i, ','))
	--set @role = convert(int,@xxx)
	
	set @role = @xxx
	select @xx = count (*) from utilisateurrole where idutilisateur = @id and idrole = @role
	if @xx = 0
	exec utilisateurrole_cre @id, @role
	set @i = @i + 1
end


commit
end try
begin catch
/*DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  
	declare @ErrorNumber int
  
    SELECT   
	@ErrorNumber = ERROR_NUMBER(),
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();  
rollback

raiserror(@ErrorMessage, @ErrorSeverity, @ErrorState,@ErrorNumber)*/
print concat('=>',error_number())
declare @ErrorMessage nvarchar(max),@ErrorSeverity int,@ErrorState int;
select @ErrorMessage = ERROR_MESSAGE()+' Line '+ cast(ERROR_LINE() as nvarchar(5))
,@ErrorSeverity = ERROR_SEVERITY(),
@ErrorState = ERROR_STATE();
--if @@trancount >0rollbacktransaction;
set @id = 0
rollback
print concat('coucou=>',@ErrorMessage)
raiserror(@ErrorMessage,@ErrorSeverity,@ErrorState);
--raiserror('flûte',1,1)
end catch

end

go
drop procedure utilisateur_eff
go
create procedure utilisateur_eff
@id int
AS
delete utilisateur 
where id=@id
;
go
drop procedure utilisateur_act
go
create procedure utilisateur_act
@id int
AS
update utilisateur set actif = 1
where id=@id
;
go
drop procedure utilisateur_desact
go
create procedure utilisateur_desact
@id int
AS
update utilisateur set actif = 0
where id=@id
;
go


go	
drop procedure messagelu_cre
go
create PROCEDURE messagelu_cre
 @idmessage int , @idlecteur int, @date datetime
AS
insert into messagelu (idmessage,idlecteur,date) values (@idmessage,@idlecteur,@date);

go
drop procedure messagelu_mod
go
create PROCEDURE messagelu_mod
@idmessage int,@idlecteur int,@date datetime
AS
update messagelu
set date=@date
where idmessage=@idmessage and idlecteur = @idlecteur
;
go
drop procedure messagelu_eff
go
create procedure messagelu_eff
@idmessage int,@idlecteur int
AS
delete messagelu 
where idmessage=@idmessage and idlecteur=@idlecteur
;
go
/*
private const string CONST_MESSAGELU_REQ = "select idmessage,idlecteur,date from messagelu";
*/

go	
drop procedure messageefface_cre
go
create PROCEDURE messageefface_cre
 @idmessage int , @ideffaceur int, @date datetime
AS
insert into messageefface (idmessage,ideffaceur,date) values (@idmessage,@ideffaceur,@date);

go
drop procedure messageefface_mod
go
create PROCEDURE messageefface_mod
@idmessage int,@ideffaceur int,@date datetime
AS
update messageefface
set date=@date
where idmessage=@idmessage and ideffaceur=@ideffaceur
;
go
drop procedure messageefface_eff
go
create procedure messageefface_eff
@idmessage int,@ideffaceur int
AS
delete messageefface 
where idmessage=@idmessage and ideffaceur=@ideffaceur
;
go
/*
private const string CONST_MESSAGEEFFACE_REQ = "select idmessage,ideffaceur,date from messageefface";
*/

go	
drop procedure utilisateurabonnement_cre
go
create PROCEDURE utilisateurabonnement_cre
 @idabonne int, @idabonnement int, @dateabonnement datetime, @cartedepayement nvarchar(100)
AS
insert into utilisateurabonnement (idabonne,idabonnement,dateabonnement,cartedepayement) values (@idabonne,@idabonnement,@dateabonnement,@cartedepayement);

go
drop procedure utilisateurabonnement_mod
go
create PROCEDURE utilisateurabonnement_mod
@idabonne int,@idabonnement int,@dateabonnement datetime,@cartedepayement nvarchar(100)
AS
update utilisateurabonnement
set dateabonnement=@dateabonnement,cartedepayement=@cartedepayement
where idabonne=@idabonne and idabonnement=@idabonnement
;
go
drop procedure utilisateurabonnement_eff
go
create procedure utilisateurabonnement_eff
@idabonne int,@idabonnement int
AS
delete utilisateurabonnement 
where idabonne=@idabonne and idabonnement=@idabonnement
;
go
/*
private const string CONST_UTILISATEURABONNEMENT_REQ = "select idabonne,idabonnement,dateabonnement,cartedepayement from utilisateurabonnement";
*/
go
/*
private const string CONST_UTILISATEURROLE_REQ = "select idutilisateur,idrole from utilisateurrole";
*/

/*go	
drop procedure utilisateurnouvelle_cre
go
create PROCEDURE utilisateurnouvelle_cre
 @idpublicateur int , @idnouvelle int, @datepublication datetime
AS
insert into utilisateurnouvelle (idpublicateur,idnouvelle,datepublication) values (@idpublicateur,@idnouvelle,@datepublication);
*/
/*go
drop procedure utilisateurnouvelle_mod
go
create PROCEDURE utilisateurnouvelle_mod
@idpublicateur int,@idnouvelle int,@datepublication datetime
AS
update utilisateurnouvelle
set datepublication=@datepublication
where idpublicateur=@idpublicateur and idnouvelle=@idnouvelle
;
*/
/*go
drop procedure utilisateurnouvelle_eff
go
create procedure utilisateurnouvelle_eff
@idpublicateur int,@idnouvelle int
AS
delete utilisateurnouvelle 
where idpublicateur=@idpublicateur and idnouvelle=@idnouvelle
;
*/
go
/*
private const string CONST_UTILISATEURNOUVELLE_REQ = "select idpublicateur,idnouvelle,datepublication from utilisateurnouvelle";
*/

/***txable***Abonnement*/
go	
drop procedure Abonnement_cre
go
create PROCEDURE Abonnement_cre
 @id int out, @nom nvarchar(100), @description nvarchar(2000), @duree int, @prix decimal, @nombremaxarbres int, @nombremaxpersonnes int
AS
insert into Abonnement (nom,description,duree,prix,nombremaxarbres,nombremaxpersonnes) values (@nom,@description,@duree,@prix,@nombremaxarbres,@nombremaxpersonnes);
set @id = @@IDENTITY;
go
drop procedure Abonnement_mod
go
create PROCEDURE Abonnement_mod
@id int,@nom nvarchar(100),@description nvarchar(2000),@duree int,@prix decimal,@nombremaxarbres int,@nombremaxpersonnes int
AS
update Abonnement
set nom=@nom,description=@description,duree=@duree,prix=@prix,nombremaxarbres=@nombremaxarbres,nombremaxpersonnes=@nombremaxpersonnes
where id=@id
;
go
drop procedure Abonnement_eff
go
create procedure Abonnement_eff
@id int
AS
delete Abonnement 
where id=@id
;
go
drop procedure Abonnement_act
go
create procedure Abonnement_act
@id int
AS
update Abonnement set actif = 1
where id=@id
;
go
drop procedure Abonnement_desact
go
create procedure Abonnement_desact
@id int
AS
update Abonnement set actif = 0
where id=@id
;
go
/*
select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnes from Abonnement
*/
/*
private const string CONST_ABONNEMENT_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnes from Abonnement";
*/
/***txable***Arbre*/
go	
drop procedure Arbre_cre
go
create PROCEDURE Arbre_cre
 @id int out, @nom nvarchar(100), @description nvarchar(2000), @idcreateur int, @datecreation datetime, @idblocage int, @idbloqueur int, @dateblocage datetime
AS
insert into Arbre (nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocage) values (@nom,@description,@idcreateur,@datecreation,@idblocage,@idbloqueur,@dateblocage);
set @id = @@IDENTITY;
go
drop procedure Arbre_mod
go
create PROCEDURE Arbre_mod
@id int,@nom nvarchar(100),@description nvarchar(2000),@idcreateur int,@datecreation datetime,@idblocage int,@idbloqueur int,@dateblocage datetime
AS
update Arbre
set nom=@nom,description=@description,idcreateur=@idcreateur,datecreation=@datecreation,idblocage=@idblocage,idbloqueur=@idbloqueur,dateblocage=@dateblocage
where id=@id
;
go
drop procedure Arbre_eff
go
create procedure Arbre_eff
@id int
AS
delete Arbre 
where id=@id
;
go
/*
private const string CONST_ARBRE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocage from Arbre";
*/
/***txable***Blocage*/
go	
drop procedure Blocage_cre
go
create PROCEDURE Blocage_cre
 @id int out, @nom nvarchar(100), @description nvarchar(100)
AS
insert into Blocage (nom,description) values (@nom,@description);
set @id = @@IDENTITY;
go
drop procedure Blocage_mod
go
create PROCEDURE Blocage_mod
@id int,@nom nvarchar(100),@description nvarchar(100)
AS
update Blocage
set nom=@nom,description=@description
where id=@id
;
go
drop procedure Blocage_eff
go
create procedure Blocage_eff
@id int
AS
delete Blocage 
where id=@id
;
go
drop procedure Blocage_act
go
create procedure Blocage_act
@id int
AS
update Blocage set actif = 1
where id=@id
;
go
drop procedure Blocage_desact
go
create procedure Blocage_desact
@id int
AS
update Blocage set actif = 0
where id=@id
;
go
/*
select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,description from Blocage
*/
/*
private const string CONST_BLOCAGE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,description from Blocage";
*/
/***txable***Conversation*/
go	
drop procedure Conversation_cre
go
create PROCEDURE Conversation_cre
 @id int out, @date datetime
AS
insert into Conversation (date) values (@date);
set @id = @@IDENTITY;
go
drop procedure Conversation_mod
go
create PROCEDURE Conversation_mod
@id int,@date datetime
AS
update Conversation
set date=@date
where id=@id
;
go
drop procedure Conversation_eff
go
create procedure Conversation_eff
@id int
AS
delete Conversation 
where id=@id
;
go
/*
private const string CONST_CONVERSATION_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,date from Conversation";
*/
/***txable***Couple*/
go	
drop procedure Couple_cre
go
create PROCEDURE Couple_cre
 @idpersonne int out, @idpartenaire int, @datedebut datetime, @datefin datetime
AS
insert into Couple (idpartenaire,datedebut,datefin) values (@idpartenaire,@datedebut,@datefin);
set @idpersonne = @@IDENTITY;
go
drop procedure Couple_mod
go
create PROCEDURE Couple_mod
@idpersonne int,@idpartenaire int,@datedebut datetime,@datefin datetime
AS
update Couple
set idpartenaire=@idpartenaire,datedebut=@datedebut,datefin=@datefin
where idpersonne=@idpersonne
;
go
drop procedure Couple_eff
go
create procedure Couple_eff
@idpersonne int
AS
delete Couple 
where idpersonne=@idpersonne
;
go
/*
private const string CONST_COUPLE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefin from Couple";
*/
/***txable***Message*/
go	
drop procedure Message_cre
go
create PROCEDURE Message_cre
 @id int out, @sujet nvarchar(100), @texte nvarchar(MAX), @idemetteur int, @idconversation int
AS
insert into Message (sujet,texte,idemetteur,idconversation) values (@sujet,@texte,@idemetteur,@idconversation);
set @id = @@IDENTITY;
go
drop procedure Message_mod
go
create PROCEDURE Message_mod
@id int,@sujet nvarchar(100),@texte nvarchar(MAX),@idemetteur int,@idconversation int
AS
update Message
set sujet=@sujet,texte=@texte,idemetteur=@idemetteur,idconversation=@idconversation
where id=@id
;
go
drop procedure Message_eff
go
create procedure Message_eff
@id int
AS
delete Message 
where id=@id
;
go
/*
private const string CONST_MESSAGE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversation from Message";
*/
/***txable***MessageForum*/
go	
drop procedure MessageForum_cre
go
create PROCEDURE MessageForum_cre
 @id int out, @sujet nvarchar(100), @texte nvarchar(MAX), @idtheme int, @idpublicateur int, @datepublication datetime
AS
insert into MessageForum (sujet,texte,idtheme,idpublicateur,datepublication) values (@sujet,@texte,@idtheme,@idpublicateur,@datepublication);
set @id = @@IDENTITY;
go
drop procedure MessageForum_mod
go
create PROCEDURE MessageForum_mod
@id int,@sujet nvarchar(100),@texte nvarchar(MAX),@idtheme int,@idpublicateur int,@datepublication datetime
AS
update MessageForum
set sujet=@sujet,texte=@texte,idtheme=@idtheme,idpublicateur=@idpublicateur,datepublication=@datepublication
where id=@id
;
go
drop procedure MessageForum_eff
go
create procedure MessageForum_eff
@id int
AS
delete MessageForum 
where id=@id
;
go
drop procedure MessageForum_act
go
create procedure MessageForum_act
@id int
AS
update MessageForum set actif = 1
where id=@id
;
go
drop procedure MessageForum_desact
go
create procedure MessageForum_desact
@id int
AS
update MessageForum set actif = 0
where id=@id
;
go
/*
select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublication from MessageForum
*/
/*
private const string CONST_MESSAGEFORUM_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublication from MessageForum";
*/
/***txable***Nouvelle*/
go	
drop procedure Nouvelle_cre
go
create PROCEDURE Nouvelle_cre
 @id int out, @titre nvarchar(100), @description nvarchar(2000), @idcreateur int
AS
insert into Nouvelle (titre,description,idcreateur) values (@titre,@description,@idcreateur);
set @id = @@IDENTITY;
go
drop procedure Nouvelle_mod
go
create PROCEDURE Nouvelle_mod
@id int,@titre nvarchar(100),@description nvarchar(2000),@idcreateur int
AS
update Nouvelle
set titre=@titre,description=@description,idcreateur=@idcreateur
where id=@id
;
go
drop procedure Nouvelle_eff
go
create procedure Nouvelle_eff
@id int
AS
delete Nouvelle 
where id=@id
;
go
drop procedure Nouvelle_act
go
create procedure Nouvelle_act
@id int
AS
update Nouvelle set actif = 1
where id=@id
;
go
drop procedure Nouvelle_desact
go
create procedure Nouvelle_desact
@id int
AS
update Nouvelle set actif = 0
where id=@id
;
go
/*
select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,description from Nouvelle
*/
/*
private const string CONST_NOUVELLE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,description from Nouvelle";
*/
/***txable***Personne*/
go	
drop procedure Personne_cre
go
create PROCEDURE Personne_cre
 @id int out, @nom nvarchar(100), @prenom nvarchar(100), @datedenaissance datetime, @datededeces datetime, @idarbre int, @dateajout datetime, @idpere int, @idmere int
AS
insert into Personne (nom,prenom,datedenaissance,datededeces,idarbre,dateajout,idpere,idmere) values (@nom,@prenom,@datedenaissance,@datededeces,@idarbre,@dateajout,@idpere,@idmere);
set @id = @@IDENTITY;
go
drop procedure Personne_mod
go
create PROCEDURE Personne_mod
@id int,@nom nvarchar(100),@prenom nvarchar(100),@datedenaissance datetime,@datededeces datetime,@idarbre int,@dateajout datetime,@idpere int,@idmere int
AS
update Personne
set nom=@nom,prenom=@prenom,datedenaissance=@datedenaissance,datededeces=@datededeces,idarbre=@idarbre,dateajout=@dateajout,idpere=@idpere,idmere=@idmere
where id=@id
;
go
drop procedure Personne_eff
go
create procedure Personne_eff
@id int
AS
delete Personne 
where id=@id
;
go
/*
private const string CONST_PERSONNE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,descriptionid,nom,prenom,datedenaissance,datededeces,idarbre,dateajout,idpere,idmere from Personne";
*/
/***txable***Role*/
go	
drop procedure Role_cre
go
create PROCEDURE Role_cre
 @id int out, @nom nvarchar(100), @description nvarchar(2000)
AS
insert into Role (nom,description) values (@nom,@description);
set @id = @@IDENTITY;
go
drop procedure Role_mod
go
create PROCEDURE Role_mod
@id int,@nom nvarchar(100),@description nvarchar(2000)
AS
update Role
set nom=@nom,description=@description
where id=@id
;
go
drop procedure Role_eff
go
create procedure Role_eff
@id int
AS
delete Role 
where id=@id
;
go
drop procedure Role_act
go
create procedure Role_act
@id int
AS
update Role set actif = 1
where id=@id
;
go
drop procedure Role_desact
go
create procedure Role_desact
@id int
AS
update Role set actif = 0
where id=@id
;
go
/*
select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,descriptionid,nom,prenom,datedenaissance,datededeces,idarbre,dateajout,idpere,idmereid,nom,description from Role
*/
/*
private const string CONST_ROLE_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,descriptionid,nom,prenom,datedenaissance,datededeces,idarbre,dateajout,idpere,idmereid,nom,description from Role";
*/
/***txable***Theme*/
go	
drop procedure Theme_cre
go
create PROCEDURE Theme_cre
 @id int out, @titre nvarchar(100), @description nvarchar(2000)
AS
insert into Theme (titre,description) values (@titre,@description);
set @id = @@IDENTITY;
go
drop procedure Theme_mod
go
create PROCEDURE Theme_mod
@id int,@titre nvarchar(100),@description nvarchar(2000)
AS
update Theme
set titre=@titre,description=@description
where id=@id
;
go
drop procedure Theme_eff
go
create procedure Theme_eff
@id int
AS
delete Theme 
where id=@id
;
go
drop procedure Theme_act
go
create procedure Theme_act
@id int
AS
update Theme set actif = 1
where id=@id
;
go
drop procedure Theme_desact
go
create procedure Theme_desact
@id int
AS
update Theme set actif = 0
where id=@id
;
go
/*
select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,descriptionid,nom,prenom,datedenaissance,datededeces,idarbre,dateajout,idpere,idmereid,nom,descriptionid,titre,description from Theme
*/
/*
private const string CONST_THEME_REQ = "select id,nom,description,duree,prix,nombremaxarbres,nombremaxpersonnesid,nom,description,idcreateur,datecreation,idblocage,idbloqueur,dateblocageid,nom,descriptionid,dateidpersonne,idpartenaire,datedebut,datefinid,sujet,texte,idemetteur,idconversationid,sujet,texte,idtheme,idpublicateur,datepublicationid,titre,descriptionid,nom,prenom,datedenaissance,datededeces,idarbre,dateajout,idpere,idmereid,nom,descriptionid,titre,description from Theme";
*/


/************SETUP******/
declare @id int
declare @iid int
exec role_cre @iid out, 'Administrateur','Voici un bel administrateur blond'
exec role_cre @iid out, 'xAdministrateur','Voici un bel administrateur blond'
exec role_cre @iid out, 'xxAdministrateur','Voici un très bel administrateur blond'


exec utilisateur_cre @id out, 'admin','admin',null,'adm@i.n',null,1,null,'1','presel','postsel', '1'
exec utilisateur_mod @id, 'admin',null,'adm@i.n',null,1,null,'1,3'
/*@id int,@nom nvarchar(100),@prenom nvarchar(100),
@email nvarchar(400),@datedenaissance datetime,@homme int, @cartedepayement nvarchar(50),
@roles nvarchar(max) */
--exec utilisateurrole_cre @id, @iid
select * from role
select * from utilisateur
select * from utilisateurrole

/*****ENDCRUD*/


