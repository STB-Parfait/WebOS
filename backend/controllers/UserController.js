const User = require('../models/User.js');

exports.createUser = async (req, res) => {
    try{
        const {email, password} = req.body;

        const newUser = await User.create({
            email: email,
            password: password
        });

        res.status(201).json({success: 'New user was created'});
    } catch(err){
        res.status(500).json({error: err.message});
    }
};

exports.getUsers = async (req, res) => {
    try{
        const users = await User.find();
        res.status(200).json(users);
    } catch(err){
        res.status(500).json({error: err.message});
    }
};

exports.getUserById = async (req, res) => {
    try{
        const user = await User.findById(req.params.id);

        if(user){
            res.status(200).json(user);
        } else{
            res.status(404).json({error: 'User was not found'});
        }
    } catch(err){
        res.status(500).json({error: err.message});
    }
};

exports.deleteUser = async (req, res) => {
    try {
        const deletedUser = await User.findByIdAndDelete(req.params.id);

        if(deletedUser){
            res.status(200).json({success: 'User was deleted'});
        } else{
            res.status(404).json({error: 'User was not found'});
        }
    } catch(err){
        res.status(500).json({error: err.message});
    }
};