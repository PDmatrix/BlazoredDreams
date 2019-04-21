create table identity_user
(
	id serial not null
		constraint pk_identity_user
			primary key,
	created_at timestamp default now() not null,
	updated_at timestamp default now() not null,
	identifier text not null
);

create table dream
(
	id serial not null
		constraint pk_dream
			primary key,
	content text not null,
	created_at timestamp default now() not null,
	updated_at timestamp default now() not null,
	user_id integer not null
		constraint dream_identity_user_id_fk
			references identity_user
);

create index idx_dream__user_id
	on dream (user_id);

create unique index identity_user_identifier_uindex
	on identity_user (identifier);

create table post
(
	id serial not null
		constraint pk_post
			primary key,
	title text not null,
	user_id integer not null
		constraint post_identity_user_id_fk
			references identity_user,
	created_at timestamp default now() not null,
	updated_at timestamp default now() not null,
	dream_id integer not null
		constraint post_dream_id_fk
			references dream,
	excerpt text not null
);

create table comment
(
	id serial not null
		constraint pk_comment
			primary key,
	content text not null,
	created_at timestamp default now() not null,
	updated_at timestamp default now() not null,
	post_id integer not null
		constraint fk_comment__post_id
			references post,
	user_id integer not null
		constraint comment_identity_user_id_fk
			references identity_user
);

create index idx_comment__post_id
	on comment (post_id);

create index idx_post__user_id
	on post (user_id);

create table tag
(
	id serial not null
		constraint pk_tag
			primary key,
	name text not null,
	created_at timestamp default now() not null,
	updated_at timestamp default now() not null
);

create table post_tags
(
	post_id integer not null
		constraint fk_post_tags__post
			references post,
	tag_id integer not null
		constraint fk_post_tags__tag
			references tag,
	constraint pk_post_tags
		primary key (post_id, tag_id)
);

create index idx_post_tags
	on post_tags (tag_id);

create function trigger_set_timestamp() returns trigger
	language plpgsql
as $$
BEGIN
  IF ROW (NEW.*) IS DISTINCT FROM ROW (OLD.*) THEN
    NEW.updated_at = now();
    RETURN NEW;
  ELSE
    RETURN OLD;
  END IF;
END;
$$;

create trigger set_timestamp
	before update
	on comment
	for each row
	execute procedure trigger_set_timestamp();

create trigger set_timestamp
	before update
	on dream
	for each row
	execute procedure trigger_set_timestamp();

create trigger set_timestamp
	before update
	on identity_user
	for each row
	execute procedure trigger_set_timestamp();

create trigger set_timestamp
	before update
	on post
	for each row
	execute procedure trigger_set_timestamp();

create trigger set_timestamp
	before update
	on tag
	for each row
	execute procedure trigger_set_timestamp();
