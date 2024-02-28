--
-- PostgreSQL database dump
--

-- Dumped from database version 14.2
-- Dumped by pg_dump version 14.2

-- Started on 2024-02-28 22:26:28

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 884 (class 1247 OID 38680)
-- Name: access_status; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.access_status AS ENUM (
    'personal',
    'group',
    'closed personal',
    'closed group'
);


ALTER TYPE public.access_status OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 38571)
-- Name: answers_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.answers_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.answers_seq OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 210 (class 1259 OID 34169)
-- Name: answers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.answers (
    answer_id integer DEFAULT nextval('public.answers_seq'::regclass) NOT NULL,
    answers_text text NOT NULL
);


ALTER TABLE public.answers OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 38578)
-- Name: student_test_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.student_test_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.student_test_seq OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 34274)
-- Name: attempts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.attempts (
    attempt_id integer DEFAULT nextval('public.student_test_seq'::regclass) NOT NULL,
    student_id integer NOT NULL,
    test_id integer NOT NULL,
    mark integer,
    "time" numeric(10,2),
    access_status public.access_status NOT NULL
);


ALTER TABLE public.attempts OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 38572)
-- Name: group_teacher_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.group_teacher_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.group_teacher_seq OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 34315)
-- Name: group_teacher; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.group_teacher (
    group_teacher_id integer DEFAULT nextval('public.group_teacher_seq'::regclass) NOT NULL,
    group_id integer NOT NULL,
    teacher_id integer NOT NULL
);


ALTER TABLE public.group_teacher OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 38573)
-- Name: groups_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.groups_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.groups_seq OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 34230)
-- Name: groups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.groups (
    group_id integer DEFAULT nextval('public.groups_seq'::regclass) NOT NULL,
    group_name text NOT NULL
);


ALTER TABLE public.groups OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 38574)
-- Name: question_answer_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.question_answer_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.question_answer_seq OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 34178)
-- Name: question_type; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.question_type (
    type_id integer NOT NULL,
    type text NOT NULL
);


ALTER TABLE public.question_type OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 34247)
-- Name: question_type_type_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.question_type ALTER COLUMN type_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.question_type_type_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 999999999
    CACHE 1
);


--
-- TOC entry 228 (class 1259 OID 38575)
-- Name: questions_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.questions_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.questions_seq OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 34160)
-- Name: questions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.questions (
    question_id integer DEFAULT nextval('public.questions_seq'::regclass) NOT NULL,
    question_text text NOT NULL,
    question_type integer NOT NULL
);


ALTER TABLE public.questions OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 38576)
-- Name: results_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.results_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.results_seq OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 34290)
-- Name: results; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.results (
    result_id integer DEFAULT nextval('public.results_seq'::regclass) NOT NULL,
    version_question_id integer NOT NULL,
    answer boolean NOT NULL,
    attempt_id integer NOT NULL
);


ALTER TABLE public.results OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 38577)
-- Name: student_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.student_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.student_seq OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 34222)
-- Name: student; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.student (
    student_id integer DEFAULT nextval('public.student_seq'::regclass) NOT NULL,
    full_name text NOT NULL,
    login text,
    password text,
    group_id integer NOT NULL
);


ALTER TABLE public.student OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 34187)
-- Name: tasks; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tasks (
    task_id integer DEFAULT nextval('public.question_answer_seq'::regclass) NOT NULL,
    question_id integer NOT NULL,
    answer_id integer NOT NULL,
    right_answer integer NOT NULL
);


ALTER TABLE public.tasks OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 38579)
-- Name: teacher_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.teacher_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.teacher_seq OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 34308)
-- Name: teacher; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.teacher (
    teacher_id integer DEFAULT nextval('public.teacher_seq'::regclass) NOT NULL,
    full_name text NOT NULL,
    login text NOT NULL,
    password text NOT NULL
);


ALTER TABLE public.teacher OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 38580)
-- Name: test_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.test_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.test_seq OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 34250)
-- Name: test; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test (
    test_id integer DEFAULT nextval('public.test_seq'::regclass) NOT NULL,
    test_name text NOT NULL,
    timer integer NOT NULL,
    teacher_id integer NOT NULL,
    comment text
);


ALTER TABLE public.test OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 34370)
-- Name: tests; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.tests AS
 SELECT test.test_name
   FROM ((public.test
     JOIN public.attempts ON ((attempts.test_id = test.test_id)))
     JOIN public.student ON ((student.group_id = attempts.student_id)))
  WHERE (student.student_id = 1);


ALTER TABLE public.tests OWNER TO postgres;

--
-- TOC entry 234 (class 1259 OID 38581)
-- Name: version_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.version_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.version_seq OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 34537)
-- Name: version; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.version (
    version_id integer DEFAULT nextval('public.version_seq'::regclass) NOT NULL,
    version_number integer NOT NULL,
    test_id integer NOT NULL
);


ALTER TABLE public.version OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 38582)
-- Name: version_question_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.version_question_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.version_question_seq OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 34258)
-- Name: version_question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.version_question (
    version_question_id integer DEFAULT nextval('public.version_question_seq'::regclass) NOT NULL,
    question_id integer NOT NULL,
    version_id integer NOT NULL
);


ALTER TABLE public.version_question OWNER TO postgres;

--
-- TOC entry 3246 (class 2606 OID 34175)
-- Name: answers answers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.answers
    ADD CONSTRAINT answers_pkey PRIMARY KEY (answer_id);


--
-- TOC entry 3248 (class 2606 OID 34184)
-- Name: question_type answers_type_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.question_type
    ADD CONSTRAINT answers_type_pkey PRIMARY KEY (type_id);


--
-- TOC entry 3256 (class 2606 OID 34236)
-- Name: groups group_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups
    ADD CONSTRAINT group_pkey PRIMARY KEY (group_id);


--
-- TOC entry 3278 (class 2606 OID 38626)
-- Name: group_teacher group_teacher_group_id_teacher_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_teacher
    ADD CONSTRAINT group_teacher_group_id_teacher_id_key UNIQUE (group_id, teacher_id);


--
-- TOC entry 3280 (class 2606 OID 34319)
-- Name: group_teacher group_teacher_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_teacher
    ADD CONSTRAINT group_teacher_pkey PRIMARY KEY (group_teacher_id);


--
-- TOC entry 3268 (class 2606 OID 34278)
-- Name: attempts group_test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attempts
    ADD CONSTRAINT group_test_pkey PRIMARY KEY (attempt_id);


--
-- TOC entry 3258 (class 2606 OID 46839)
-- Name: groups groups_group_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups
    ADD CONSTRAINT groups_group_name_key UNIQUE (group_name);


--
-- TOC entry 3252 (class 2606 OID 34228)
-- Name: student people_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.student
    ADD CONSTRAINT people_pkey PRIMARY KEY (student_id);


--
-- TOC entry 3250 (class 2606 OID 34218)
-- Name: tasks question_answer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT question_answer_pkey PRIMARY KEY (task_id);


--
-- TOC entry 3244 (class 2606 OID 34166)
-- Name: questions questions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.questions
    ADD CONSTRAINT questions_pkey PRIMARY KEY (question_id);


--
-- TOC entry 3270 (class 2606 OID 34296)
-- Name: results results_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.results
    ADD CONSTRAINT results_pkey PRIMARY KEY (result_id);


--
-- TOC entry 3272 (class 2606 OID 46837)
-- Name: results results_version_question_id_attempt_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.results
    ADD CONSTRAINT results_version_question_id_attempt_id_key UNIQUE (version_question_id, attempt_id);


--
-- TOC entry 3254 (class 2606 OID 46833)
-- Name: student student_login_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_login_key UNIQUE (login);


--
-- TOC entry 3274 (class 2606 OID 46831)
-- Name: teacher teacher_login_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teacher
    ADD CONSTRAINT teacher_login_key UNIQUE (login);


--
-- TOC entry 3276 (class 2606 OID 34314)
-- Name: teacher teacher_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teacher
    ADD CONSTRAINT teacher_pkey PRIMARY KEY (teacher_id);


--
-- TOC entry 3260 (class 2606 OID 34256)
-- Name: test test_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_pkey PRIMARY KEY (test_id);


--
-- TOC entry 3264 (class 2606 OID 34262)
-- Name: version_question test_question_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.version_question
    ADD CONSTRAINT test_question_pkey PRIMARY KEY (version_question_id);


--
-- TOC entry 3262 (class 2606 OID 46835)
-- Name: test test_test_name_teacher_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_test_name_teacher_id_key UNIQUE (test_name, teacher_id);


--
-- TOC entry 3282 (class 2606 OID 34541)
-- Name: version version_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.version
    ADD CONSTRAINT version_pkey PRIMARY KEY (version_id);


--
-- TOC entry 3266 (class 2606 OID 46827)
-- Name: version_question version_question_question_id_version_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.version_question
    ADD CONSTRAINT version_question_question_id_version_id_key UNIQUE (question_id, version_id);


--
-- TOC entry 3294 (class 2606 OID 38639)
-- Name: group_teacher group_teacher_group_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_teacher
    ADD CONSTRAINT group_teacher_group_id_fkey FOREIGN KEY (group_id) REFERENCES public.groups(group_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3295 (class 2606 OID 38644)
-- Name: group_teacher group_teacher_teacher_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_teacher
    ADD CONSTRAINT group_teacher_teacher_id_fkey FOREIGN KEY (teacher_id) REFERENCES public.teacher(teacher_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3284 (class 2606 OID 38649)
-- Name: tasks question_answer_answer_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT question_answer_answer_id_fkey FOREIGN KEY (answer_id) REFERENCES public.answers(answer_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3285 (class 2606 OID 38654)
-- Name: tasks question_answer_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tasks
    ADD CONSTRAINT question_answer_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.questions(question_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3283 (class 2606 OID 46845)
-- Name: questions questions_question_type_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.questions
    ADD CONSTRAINT questions_question_type_fkey FOREIGN KEY (question_type) REFERENCES public.question_type(type_id) ON UPDATE RESTRICT ON DELETE RESTRICT NOT VALID;


--
-- TOC entry 3293 (class 2606 OID 46821)
-- Name: results results_student_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.results
    ADD CONSTRAINT results_student_test_id_fkey FOREIGN KEY (attempt_id) REFERENCES public.attempts(attempt_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3292 (class 2606 OID 38664)
-- Name: results results_version_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.results
    ADD CONSTRAINT results_version_question_id_fkey FOREIGN KEY (version_question_id) REFERENCES public.version_question(version_question_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3286 (class 2606 OID 38669)
-- Name: student student_group_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_group_id_fkey FOREIGN KEY (group_id) REFERENCES public.groups(group_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3291 (class 2606 OID 38620)
-- Name: attempts student_test_student_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attempts
    ADD CONSTRAINT student_test_student_id_fkey FOREIGN KEY (student_id) REFERENCES public.student(student_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3290 (class 2606 OID 38615)
-- Name: attempts student_test_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.attempts
    ADD CONSTRAINT student_test_test_id_fkey FOREIGN KEY (test_id) REFERENCES public.test(test_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3287 (class 2606 OID 38595)
-- Name: test test_teacher_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test
    ADD CONSTRAINT test_teacher_id_fkey FOREIGN KEY (teacher_id) REFERENCES public.teacher(teacher_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3289 (class 2606 OID 38674)
-- Name: version_question version_question_question_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.version_question
    ADD CONSTRAINT version_question_question_id_fkey FOREIGN KEY (question_id) REFERENCES public.questions(question_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3288 (class 2606 OID 38610)
-- Name: version_question version_question_version_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.version_question
    ADD CONSTRAINT version_question_version_id_fkey FOREIGN KEY (version_id) REFERENCES public.version(version_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3296 (class 2606 OID 38605)
-- Name: version version_test_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.version
    ADD CONSTRAINT version_test_id_fkey FOREIGN KEY (test_id) REFERENCES public.test(test_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


-- Completed on 2024-02-28 22:26:29

--
-- PostgreSQL database dump complete
--

