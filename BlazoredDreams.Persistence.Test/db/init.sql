--
-- PostgreSQL database dump
--

-- Dumped from database version 11.1
-- Dumped by pg_dump version 11.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: trigger_set_timestamp(); Type: FUNCTION; Schema: public; Owner: blazoreddreams
--

CREATE FUNCTION public.trigger_set_timestamp() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
  NEW.updated_at = NOW();
  RETURN NEW;
END;
$$;


ALTER FUNCTION public.trigger_set_timestamp() OWNER TO blazoreddreams;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: comment; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.comment (
    id integer NOT NULL,
    content text NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL,
    post_id integer NOT NULL,
    user_id integer NOT NULL
);


ALTER TABLE public.comment OWNER TO blazoreddreams;

--
-- Name: comment_id_seq; Type: SEQUENCE; Schema: public; Owner: blazoreddreams
--

CREATE SEQUENCE public.comment_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.comment_id_seq OWNER TO blazoreddreams;

--
-- Name: comment_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blazoreddreams
--

ALTER SEQUENCE public.comment_id_seq OWNED BY public.comment.id;


--
-- Name: dream; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.dream (
    id integer NOT NULL,
    content text NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL,
    user_id integer NOT NULL
);


ALTER TABLE public.dream OWNER TO blazoreddreams;

--
-- Name: dream_id_seq; Type: SEQUENCE; Schema: public; Owner: blazoreddreams
--

CREATE SEQUENCE public.dream_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.dream_id_seq OWNER TO blazoreddreams;

--
-- Name: dream_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blazoreddreams
--

ALTER SEQUENCE public.dream_id_seq OWNED BY public.dream.id;


--
-- Name: identity_user; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.identity_user (
    id integer NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL,
    identifier text NOT NULL
);


ALTER TABLE public.identity_user OWNER TO blazoreddreams;

--
-- Name: identity_user_id_seq; Type: SEQUENCE; Schema: public; Owner: blazoreddreams
--

CREATE SEQUENCE public.identity_user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.identity_user_id_seq OWNER TO blazoreddreams;

--
-- Name: identity_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blazoreddreams
--

ALTER SEQUENCE public.identity_user_id_seq OWNED BY public.identity_user.id;


--
-- Name: post; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.post (
    id integer NOT NULL,
    title text NOT NULL,
    user_id integer NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL,
    dream_id integer NOT NULL,
    excerpt text NOT NULL
);


ALTER TABLE public.post OWNER TO blazoreddreams;

--
-- Name: post_id_seq; Type: SEQUENCE; Schema: public; Owner: blazoreddreams
--

CREATE SEQUENCE public.post_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.post_id_seq OWNER TO blazoreddreams;

--
-- Name: post_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blazoreddreams
--

ALTER SEQUENCE public.post_id_seq OWNED BY public.post.id;


--
-- Name: post_tags; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.post_tags (
    post_id integer NOT NULL,
    tag_id integer NOT NULL
);


ALTER TABLE public.post_tags OWNER TO blazoreddreams;

--
-- Name: tag; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.tag (
    id integer NOT NULL,
    name text NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL,
    updated_at timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.tag OWNER TO blazoreddreams;

--
-- Name: tag_id_seq; Type: SEQUENCE; Schema: public; Owner: blazoreddreams
--

CREATE SEQUENCE public.tag_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tag_id_seq OWNER TO blazoreddreams;

--
-- Name: tag_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blazoreddreams
--

ALTER SEQUENCE public.tag_id_seq OWNED BY public.tag.id;


--
-- Name: user_likes; Type: TABLE; Schema: public; Owner: blazoreddreams
--

CREATE TABLE public.user_likes (
    user_id integer NOT NULL,
    post_id integer NOT NULL
);


ALTER TABLE public.user_likes OWNER TO blazoreddreams;

--
-- Name: comment id; Type: DEFAULT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.comment ALTER COLUMN id SET DEFAULT nextval('public.comment_id_seq'::regclass);


--
-- Name: dream id; Type: DEFAULT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.dream ALTER COLUMN id SET DEFAULT nextval('public.dream_id_seq'::regclass);


--
-- Name: identity_user id; Type: DEFAULT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.identity_user ALTER COLUMN id SET DEFAULT nextval('public.identity_user_id_seq'::regclass);


--
-- Name: post id; Type: DEFAULT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post ALTER COLUMN id SET DEFAULT nextval('public.post_id_seq'::regclass);


--
-- Name: tag id; Type: DEFAULT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.tag ALTER COLUMN id SET DEFAULT nextval('public.tag_id_seq'::regclass);


--
-- Data for Name: comment; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Data for Name: dream; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Data for Name: identity_user; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Data for Name: post; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Data for Name: post_tags; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Data for Name: tag; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Data for Name: user_likes; Type: TABLE DATA; Schema: public; Owner: blazoreddreams
--



--
-- Name: comment_id_seq; Type: SEQUENCE SET; Schema: public; Owner: blazoreddreams
--

SELECT pg_catalog.setval('public.comment_id_seq', 1, false);


--
-- Name: dream_id_seq; Type: SEQUENCE SET; Schema: public; Owner: blazoreddreams
--

SELECT pg_catalog.setval('public.dream_id_seq', 1, false);


--
-- Name: identity_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: blazoreddreams
--

SELECT pg_catalog.setval('public.identity_user_id_seq', 1, false);


--
-- Name: post_id_seq; Type: SEQUENCE SET; Schema: public; Owner: blazoreddreams
--

SELECT pg_catalog.setval('public.post_id_seq', 1, false);


--
-- Name: tag_id_seq; Type: SEQUENCE SET; Schema: public; Owner: blazoreddreams
--

SELECT pg_catalog.setval('public.tag_id_seq', 1, false);


--
-- Name: comment pk_comment; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.comment
    ADD CONSTRAINT pk_comment PRIMARY KEY (id);


--
-- Name: dream pk_dream; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.dream
    ADD CONSTRAINT pk_dream PRIMARY KEY (id);


--
-- Name: identity_user pk_identity_user; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.identity_user
    ADD CONSTRAINT pk_identity_user PRIMARY KEY (id);


--
-- Name: post pk_post; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post
    ADD CONSTRAINT pk_post PRIMARY KEY (id);


--
-- Name: post_tags pk_post_tags; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post_tags
    ADD CONSTRAINT pk_post_tags PRIMARY KEY (post_id, tag_id);


--
-- Name: tag pk_tag; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.tag
    ADD CONSTRAINT pk_tag PRIMARY KEY (id);


--
-- Name: user_likes table_name_pk; Type: CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.user_likes
    ADD CONSTRAINT table_name_pk PRIMARY KEY (user_id, post_id);


--
-- Name: identity_user_identifier_uindex; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE UNIQUE INDEX identity_user_identifier_uindex ON public.identity_user USING btree (identifier);


--
-- Name: idx_comment__post_id; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE INDEX idx_comment__post_id ON public.comment USING btree (post_id);


--
-- Name: idx_dream__user_id; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE INDEX idx_dream__user_id ON public.dream USING btree (user_id);


--
-- Name: idx_post__user_id; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE INDEX idx_post__user_id ON public.post USING btree (user_id);


--
-- Name: idx_post_tags; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE INDEX idx_post_tags ON public.post_tags USING btree (tag_id);


--
-- Name: table_name_post_id_uindex; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE UNIQUE INDEX table_name_post_id_uindex ON public.user_likes USING btree (post_id);


--
-- Name: table_name_user_id_uindex; Type: INDEX; Schema: public; Owner: blazoreddreams
--

CREATE UNIQUE INDEX table_name_user_id_uindex ON public.user_likes USING btree (user_id);


--
-- Name: comment set_timestamp; Type: TRIGGER; Schema: public; Owner: blazoreddreams
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public.comment FOR EACH ROW EXECUTE PROCEDURE public.trigger_set_timestamp();


--
-- Name: dream set_timestamp; Type: TRIGGER; Schema: public; Owner: blazoreddreams
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public.dream FOR EACH ROW EXECUTE PROCEDURE public.trigger_set_timestamp();


--
-- Name: identity_user set_timestamp; Type: TRIGGER; Schema: public; Owner: blazoreddreams
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public.identity_user FOR EACH ROW EXECUTE PROCEDURE public.trigger_set_timestamp();


--
-- Name: post set_timestamp; Type: TRIGGER; Schema: public; Owner: blazoreddreams
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public.post FOR EACH ROW EXECUTE PROCEDURE public.trigger_set_timestamp();


--
-- Name: tag set_timestamp; Type: TRIGGER; Schema: public; Owner: blazoreddreams
--

CREATE TRIGGER set_timestamp BEFORE UPDATE ON public.tag FOR EACH ROW EXECUTE PROCEDURE public.trigger_set_timestamp();


--
-- Name: comment comment_identity_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.comment
    ADD CONSTRAINT comment_identity_user_id_fk FOREIGN KEY (user_id) REFERENCES public.identity_user(id);


--
-- Name: dream dream_identity_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.dream
    ADD CONSTRAINT dream_identity_user_id_fk FOREIGN KEY (user_id) REFERENCES public.identity_user(id);


--
-- Name: comment fk_comment__post_id; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.comment
    ADD CONSTRAINT fk_comment__post_id FOREIGN KEY (post_id) REFERENCES public.post(id);


--
-- Name: post_tags fk_post_tags__post; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post_tags
    ADD CONSTRAINT fk_post_tags__post FOREIGN KEY (post_id) REFERENCES public.post(id);


--
-- Name: post_tags fk_post_tags__tag; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post_tags
    ADD CONSTRAINT fk_post_tags__tag FOREIGN KEY (tag_id) REFERENCES public.tag(id);


--
-- Name: post post_dream_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_dream_id_fk FOREIGN KEY (dream_id) REFERENCES public.dream(id);


--
-- Name: post post_identity_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.post
    ADD CONSTRAINT post_identity_user_id_fk FOREIGN KEY (user_id) REFERENCES public.identity_user(id);


--
-- Name: user_likes table_name_post_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.user_likes
    ADD CONSTRAINT table_name_post_id_fk FOREIGN KEY (post_id) REFERENCES public.post(id);


--
-- Name: user_likes user_likes_identity_user_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: blazoreddreams
--

ALTER TABLE ONLY public.user_likes
    ADD CONSTRAINT user_likes_identity_user_id_fk FOREIGN KEY (user_id) REFERENCES public.identity_user(id);


--
-- PostgreSQL database dump complete
--

