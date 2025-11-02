const User = require('../models/User.js');

exports.createUser = async (req, res) => {
    try{
        const {username, email, password} = req.body;

        const newUser = await User.create({
            username: username,
            email: email,
            password: password
        });

        return res.status(201).json({success: 'New user was created'});
    } catch(err){
        return res.status(500).json({error: err.message});
    }
};

exports.getUsers = async (req, res) => {
    try{
        const users = await User.find();
        return res.status(200).json(users);
    } catch(err){
        return res.status(500).json({error: err.message});
    }
};

exports.getUserById = async (req, res) => {
    try{
        const user = await User.findById(req.params.id);

        if(user){
            return res.status(200).json(user);
        } else{
            return res.status(404).json({error: 'User was not found'});
        }
    } catch(err){
        return res.status(500).json({error: err.message});
    }
};

exports.deleteUser = async (req, res) => {
    try {
        const deletedUser = await User.findByIdAndDelete(req.params.id);

        if(deletedUser){
            return res.status(200).json({success: 'User was deleted'});
        } else{
            return res.status(404).json({error: 'User was not found'});
        }
    } catch(err){
        return res.status(500).json({error: err.message});
    }
};

exports.checkEmails = async (req, res) => {
    try{
        const { email } = req.body;

        const user = await User.findOne({ email: email.toLowerCase()});

        if(user){
            return res.json({isAvaliable: false});
        } else{
            return res.json({isAvaliable: true});
        }
    } catch(err){
        return res.status(500).json({error: err.message});
    }
};