-- insert user profile
insert into Fakebook.Profile(Email,ProfilePictureUrl,FirstName,LastName,PhoneNumber,BirthDate,Status) VALUES ('Damien@gmail.com','https://www.houseloan.com/img/headshot-male.jpg','Demien','Bevins','1231231234','1/1/2021','Working on project');
insert into Fakebook.Profile(Email,ProfilePictureUrl,FirstName,LastName,PhoneNumber,BirthDate,Status) VALUES ('Daniel@gmail.com','https://www.houseloan.com/img/headshot-male.jpg','Daniel','Peterson','4564564567','1/2/2021','Working on QC');
insert into Fakebook.Profile(Email,ProfilePictureUrl,FirstName,LastName,PhoneNumber,BirthDate,Status) VALUES ('Luke@gmail.com','https://www.houseloan.com/img/headshot-male.jpg','Luke','Fisher','7897897890','1/3/2021','Working on fire');
insert into Fakebook.Profile(Email,ProfilePictureUrl,FirstName,LastName,PhoneNumber,BirthDate,Status) VALUES ('Hao@gmail.com','https://www.houseloan.com/img/headshot-male.jpg','Hao','Yang','0120120123','1/4/2021','Working on data');

-- insert into follow
insert into Fakebook.Follow(FolloweeEmail,FollowerEmail) values('Damien@gmail.com','Daniel@gmail.com');
insert into Fakebook.Follow(FolloweeEmail,FollowerEmail) values('Damien@gmail.com','Luke@gmail.com');
insert into Fakebook.Follow(FolloweeEmail,FollowerEmail) values('Damien@gmail.com','Hao@gmail.com');

insert into Fakebook.Follow(FolloweeEmail,FollowerEmail) values('Daniel@gmail.com','Damien@gmail.com');
insert into Fakebook.Follow(FolloweeEmail,FollowerEmail) values('Luke@gmail.com','Damien@gmail.com');
insert into Fakebook.Follow(FolloweeEmail,FollowerEmail) values('Hao@gmail.com', 'Damien@gmail.com');

-- check profiles
select * from Fakebook.Profile;

-- check Damien's connection
select * 
from Fakebook.Profile p
join Fakebook.Follow f on p.Email = f.FolloweeEmail
where p.Email = 'Damien@gmail.com';

-- check Daniel's connection
select * from Fakebook.Profile p 
join Fakebook.Follow f on p.Email = f.FolloweeEmail
where p.Email = 'Daniel@gmail.com';